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
            Assert.Null(result.Failure.Message);
            Assert.Null(result.Failure.Exception);
            Assert.False(result.IsSuccessful);
            Assert.True(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_With_Reason_Should_Succeed()
    {
        var reason = "Failure reason.";
        var result = Result.CreateFail(reason);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Failure);
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
        var result = Result.CreateFail(exception);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Failure);
            Assert.Null(result.Failure.Message);
            Assert.NotNull(result.Failure.Exception);
            Assert.False(result.IsSuccessful);
            Assert.True(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_With_Reason_And_Exception_Should_Succeed()
    {
        var reason = "Failure reason.";
        var exception = new Exception();
        var result = Result.CreateFail(reason, exception);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Failure);
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
}