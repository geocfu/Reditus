using Reditus.Definitions;
using Xunit;

namespace Reditus.UnitTests;

public class ResultTests
{
    [Fact]
    public void Creating_Successful_Result_Should_Succeed()
    {
        var result = Result.Success();

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.Throws<InvalidOperationException>(() => result.Error);
            Assert.True(result.IsSuccessful);
            Assert.False(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_From_Successful_Result_Should_Fail()
    {
        var oldResult = Result.Success();
        Assert.Throws<InvalidOperationException>(() => Result.Failure(oldResult));
    }

    [Fact]
    public void Creating_Failed_Result_Should_Succeed()
    {
        var result = Result.Failure();

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Equal("An error occurred.", result.Error.Message);
            Assert.Null(result.Error.Exception);
            Assert.False(result.IsSuccessful);
            Assert.True(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_With_Reason_Should_Succeed()
    {
        const string reason = "Error reason.";
        var failure = new Error(reason);
        var result = Result.Failure(failure);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Equal("Error reason.", result.Error.Message);
            Assert.Null(result.Error.Exception);
            Assert.False(result.IsSuccessful);
            Assert.True(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_With_Exception_Should_Succeed()
    {
        var exception = new Exception();
        var failure = new Error(exception);
        var result = Result.Failure(failure);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Equal("An error occurred.", result.Error.Message);
            Assert.NotNull(result.Error.Exception);
            Assert.False(result.IsSuccessful);
            Assert.True(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_With_Reason_And_Exception_Should_Succeed()
    {
        const string reason = "Error reason.";
        var exception = new Exception();
        var failure = new Error(exception, reason);
        var result = Result.Failure(failure);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Equal("Error reason.", result.Error.Message);
            Assert.NotNull(result.Error.Exception);
            Assert.False(result.IsSuccessful);
            Assert.True(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_With_Error_Should_Succeed()
    {
        var error = new Error();
        var result = Result.Failure(error);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Equal("An error occurred.", result.Error.Message);
            Assert.Null(result.Error.Exception);
            Assert.False(result.IsSuccessful);
            Assert.True(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_With_Null_Error_Should_Fail()
    {
        Error error = null!;
        Assert.Throws<ArgumentNullException>(() => Result.Failure(error));
    }

    [Fact]
    public void Creating_Failed_Result_From_A_Different_Failed_Result_Should_Succeed()
    {
        var oldResult = Result.Failure();
        var newResult = Result.Failure(oldResult);

        Assert.Multiple(() =>
        {
            Assert.NotNull(newResult);
            Assert.NotNull(newResult.Error);
            Assert.Equal("An error occurred.", newResult.Error.Message);
            Assert.Null(newResult.Error.Exception);
            Assert.False(newResult.IsSuccessful);
            Assert.True(newResult.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_From_Null_Result_Should_Fail()
    {
        Result oldResult = null!;
        Assert.Throws<ArgumentNullException>(() => Result.Failure(oldResult));
    }

    [Fact]
    public void Creating_Failed_Result_From_Exception_Of_Canceled_Token_Should_Have_OperationCanceledError()
    {
        var exception = new OperationCanceledException();
        var result = Result.Failure(exception, new CancellationToken(true));

        Assert.Multiple(() =>
        {
            Assert.True(result.IsFailed);
            Assert.IsType<OperationCanceledError>(result.Error);
            Assert.Same(exception, result.Error.Exception);
        });
    }

    [Fact]
    public void Creating_Failed_Result_From_OperationCanceledException_Of_Other_Source_Should_Have_Error()
    {
        // e.g. an HttpClient timeout: the exception is a cancellation, but the caller's token was not canceled
        var exception = new OperationCanceledException();
        var result = Result.Failure(exception, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.True(result.IsFailed);
            Assert.IsType<Error>(result.Error);
            Assert.Same(exception, result.Error.Exception);
        });
    }

    [Fact]
    public void Creating_Failed_Result_From_Other_Exception_Of_Canceled_Token_Should_Have_Error()
    {
        var exception = new InvalidOperationException();
        var result = Result.Failure(exception, new CancellationToken(true));

        Assert.Multiple(() =>
        {
            Assert.True(result.IsFailed);
            Assert.IsType<Error>(result.Error);
            Assert.Same(exception, result.Error.Exception);
        });
    }

    [Fact]
    public void Creating_Failed_Result_With_Error_And_Canceled_Token_Should_Have_OperationCanceledError()
    {
        var result = Result.Failure(new Error("Error reason."), new CancellationToken(true));

        Assert.IsType<OperationCanceledError>(result.Error);
    }

    [Fact]
    public void Creating_Failed_Result_With_Error_And_Not_Canceled_Token_Should_Have_The_Error()
    {
        var error = new Error("Error reason.");
        var result = Result.Failure(error, CancellationToken.None);

        Assert.Same(error, result.Error);
    }

    [Fact]
    public void Creating_Failed_Result_With_Null_Error_And_Token_Should_Fail()
    {
        Error error = null!;

        Assert.Multiple(() =>
        {
            Assert.Throws<ArgumentNullException>(() => Result.Failure(error, CancellationToken.None));
            Assert.Throws<ArgumentNullException>(() => Result.Failure(error, new CancellationToken(true)));
        });
    }

    [Fact]
    public void Creating_Failed_Result_From_Null_Exception_Should_Fail()
    {
        Exception exception = null!;
        Assert.Throws<ArgumentNullException>(() => Result.Failure(exception, CancellationToken.None));
    }
}