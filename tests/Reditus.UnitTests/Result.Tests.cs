using Reditus.Definitions;
using Xunit;

namespace Reditus.UnitTests;

public class ResultTests
{
    [Fact]
    public void Creating_Successful_Result_Should_Succeed()
    {
        var result = Result.CreateSuccess();

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.Throws<InvalidOperationException>(() => result.Failure);
            Assert.True(result.IsSuccessful);
            Assert.False(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Successful_Result_With_ISuccess_Should_Succeed()
    {
        var success = new Success();
        var result = Result.CreateSuccess(success);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.Throws<InvalidOperationException>(() => result.Failure);
            Assert.True(result.IsSuccessful);
            Assert.False(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_Should_Succeed()
    {
        var result = Result.CreateFail();

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Failure);
            Assert.Throws<InvalidOperationException>(() => result.Success);
            Assert.Null(result.Failure.Message);
            Assert.Null(result.Failure.Exception);
            Assert.False(result.IsSuccessful);
            Assert.True(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_With_Reason_Should_Succeed()
    {
        const string reason = "Failure reason.";
        var failure = new Failure(reason);
        var result = Result.CreateFail(failure);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Failure);
            Assert.Throws<InvalidOperationException>(() => result.Success);
            Assert.Equal("Failure reason.", result.Failure.Message);
            Assert.Null(result.Failure.Exception);
            Assert.False(result.IsSuccessful);
            Assert.True(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_With_Exception_Should_Succeed()
    {
        var exception = new Exception();
        var failure = new Failure(exception);
        var result = Result.CreateFail(failure);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Failure);
            Assert.Throws<InvalidOperationException>(() => result.Success);
            Assert.Null(result.Failure.Message);
            Assert.NotNull(result.Failure.Exception);
            Assert.False(result.IsSuccessful);
            Assert.True(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_With_Reason_And_Exception_Should_Succeed()
    {
        const string reason = "Failure reason.";
        var exception = new Exception();
        var failure = new Failure(reason, exception);
        var result = Result.CreateFail(failure);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Failure);
            Assert.Throws<InvalidOperationException>(() => result.Success);
            Assert.Equal("Failure reason.", result.Failure.Message);
            Assert.NotNull(result.Failure.Exception);
            Assert.False(result.IsSuccessful);
            Assert.True(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_With_IFailure_Should_Succeed()
    {
        var error = new Failure();
        var result = Result.CreateFail(error);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Failure);
            Assert.Throws<InvalidOperationException>(() => result.Success);
            Assert.Null(result.Failure.Message);
            Assert.Null(result.Failure.Exception);
            Assert.False(result.IsSuccessful);
            Assert.True(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_With_Null_Failure_Should_Fail()
    {
        Failure failure = null!;
        Assert.Throws<ArgumentNullException>(() => Result.CreateFail(failure));
    }

    [Fact]
    public void Creating_Failed_Result_From_A_Different_Failed_Result_Should_Succeed()
    {
        var oldResult = Result.CreateFail();
        var newResult = Result.CreateFail(oldResult);

        Assert.Multiple(() =>
        {
            Assert.NotNull(newResult);
            Assert.NotNull(newResult.Failure);
            Assert.Throws<InvalidOperationException>(() => newResult.Success);
            Assert.Null(newResult.Failure.Message);
            Assert.Null(newResult.Failure.Exception);
            Assert.False(newResult.IsSuccessful);
            Assert.True(newResult.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_From_Null_Result_Should_Fail()
    {
        Result oldResult = null!;
        Assert.Throws<ArgumentNullException>(() => Result.CreateFail(oldResult));
    }

    [Fact]
    public void Creating_Successful_Result_From_A_Different_Successful_Result_Should_Succeed()
    {
        var oldResult = Result.CreateSuccess();
        var newResult = Result.CreateSuccess(oldResult);

        Assert.Multiple(() =>
        {
            Assert.NotNull(newResult);
            Assert.Throws<InvalidOperationException>(() => newResult.Failure);
            Assert.True(newResult.IsSuccessful);
            Assert.False(newResult.IsFailed);
        });
    }
    
    [Fact]
    public void Creating_Successful_Result_From_Null_Result_Should_Fail()
    {
        Result oldResult = null!;
        Assert.Throws<ArgumentNullException>(() => Result.CreateSuccess(oldResult));
    }
}