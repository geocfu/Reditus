using Reditus.Definitions;
using Xunit;

namespace Reditus.UnitTests;

public class ResultTTests
{
    [Fact]
    public void Creating_Successful_Result_Of_T_Should_Succeed()
    {
        var value = new object();
        var result = Result<object>.Success(value);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.Same(value, result.Value);
            Assert.Throws<InvalidOperationException>(() => result.Error);
            Assert.True(result.IsSuccessful);
            Assert.False(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Successful_Result_Of_T_With_Null_Value_Should_Fail()
    {
        object value = null!;
        Assert.Throws<ArgumentNullException>(() => Result<object>.Success(value));
    }

    [Fact]
    public void Reading_Value_Of_Failed_Result_Of_T_Should_Fail()
    {
        var result = Result<object>.Failure();
        Assert.Throws<InvalidOperationException>(() => result.Value);
    }

    [Fact]
    public void Creating_Failed_Result_Of_T_From_Successful_Result_Should_Fail()
    {
        var oldResult = Result<int>.Success(1);
        Assert.Throws<InvalidOperationException>(() => Result<object>.Failure(oldResult));
    }

    [Fact]
    public void Creating_Failed_Result_Of_T_Should_Succeed()
    {
        var result = Result<object>.Failure();

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
    public void Creating_Failed_Result_Of_T_With_Reason_Should_Succeed()
    {
        const string reason = "Error reason.";
        var failure = new Error(reason);
        var result = Result<object>.Failure(failure);

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
    public void Creating_Failed_Result_Of_T_With_Exception_Should_Succeed()
    {
        var exception = new Exception();
        var failure = new Error(exception);
        var result = Result<object>.Failure(failure);

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
    public void Creating_Failed_Result_Of_T_With_Reason_And_Exception_Should_Succeed()
    {
        const string reason = "Error reason.";
        var exception = new Exception();
        var failure = new Error(exception, reason);
        var result = Result<object>.Failure(failure);

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
    public void Creating_Failed_Result_Of_T_With_Error_Should_Succeed()
    {
        var error = new Error();
        var result = Result<object>.Failure(error);

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
    public void Creating_Failed_Result_Of_T_With_Null_Error_Should_Fail()
    {
        Error error = null!;
        Assert.Throws<ArgumentNullException>(() => Result<object>.Failure(error));
    }

    [Fact]
    public void Creating_Failed_Result_Of_T_From_A_Different_Failed_Result_Of_Y_Should_Succeed()
    {
        var oldResult = Result<int>.Failure();
        var newResult = Result<bool>.Failure(oldResult);

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
    public void Creating_Failed_Result_Of_T_From_Null_Result_Should_Fail()
    {
        Result<object> oldResult = null!;
        Assert.Throws<ArgumentNullException>(() => Result<object>.Failure(oldResult));
    }

    [Fact]
    public void Creating_Failed_Result_Of_T_From_Exception_Of_Canceled_Token_Should_Have_OperationCanceledError()
    {
        var exception = new OperationCanceledException();
        var result = Result<object>.Failure(exception, new CancellationToken(true));

        Assert.Multiple(() =>
        {
            Assert.True(result.IsFailed);
            Assert.IsType<OperationCanceledError>(result.Error);
            Assert.Same(exception, result.Error.Exception);
        });
    }

    [Fact]
    public void Creating_Failed_Result_Of_T_From_OperationCanceledException_Of_Other_Source_Should_Have_Error()
    {
        // e.g. an HttpClient timeout: the exception is a cancellation, but the caller's token was not canceled
        var exception = new OperationCanceledException();
        var result = Result<object>.Failure(exception, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.True(result.IsFailed);
            Assert.IsType<Error>(result.Error);
            Assert.Same(exception, result.Error.Exception);
        });
    }

    [Fact]
    public void Creating_Failed_Result_Of_T_From_Other_Exception_Of_Canceled_Token_Should_Have_Error()
    {
        var exception = new InvalidOperationException();
        var result = Result<object>.Failure(exception, new CancellationToken(true));

        Assert.Multiple(() =>
        {
            Assert.True(result.IsFailed);
            Assert.IsType<Error>(result.Error);
            Assert.Same(exception, result.Error.Exception);
        });
    }

    [Fact]
    public void Creating_Failed_Result_Of_T_With_Error_And_Canceled_Token_Should_Have_OperationCanceledError()
    {
        var result = Result<object>.Failure(new Error("Error reason."), new CancellationToken(true));

        Assert.IsType<OperationCanceledError>(result.Error);
    }

    [Fact]
    public void Creating_Failed_Result_Of_T_With_Error_And_Not_Canceled_Token_Should_Have_The_Error()
    {
        var error = new Error("Error reason.");
        var result = Result<object>.Failure(error, CancellationToken.None);

        Assert.Same(error, result.Error);
    }

    [Fact]
    public void Creating_Failed_Result_Of_T_With_Null_Error_And_Token_Should_Fail()
    {
        Error error = null!;

        Assert.Multiple(() =>
        {
            Assert.Throws<ArgumentNullException>(() => Result<object>.Failure(error, CancellationToken.None));
            Assert.Throws<ArgumentNullException>(() => Result<object>.Failure(error, new CancellationToken(true)));
        });
    }

    [Fact]
    public void Creating_Failed_Result_Of_T_From_Null_Exception_Should_Fail()
    {
        Exception exception = null!;
        Assert.Throws<ArgumentNullException>(() => Result<object>.Failure(exception, CancellationToken.None));
    }
}