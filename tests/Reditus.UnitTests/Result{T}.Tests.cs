using Reditus.Definitions;
using Xunit;

namespace Reditus.UnitTests;

public class ResultTTests
{
    [Fact]
    public void Creating_Successful_Result_Of_T_Should_Succeed()
    {
        var value = new object();
        var result = Result<object>.CreateSuccess(value);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.Throws<InvalidOperationException>(() => result.Failure);
            Assert.True(result.IsSuccessful);
            Assert.False(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Successful_Result_Of_T_With_ISuccess_Of_T_Should_Succeed()
    {
        var value = new object();
        var success = new Success<object>(value);
        var result = Result<object>.CreateSuccess(success);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.Throws<InvalidOperationException>(() => result.Failure);
            Assert.True(result.IsSuccessful);
            Assert.False(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_Of_T_Should_Succeed()
    {
        var result = Result<object>.CreateFail();

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
    public void Creating_Failed_Result_Of_T_With_IFailure_Should_Succeed()
    {
        var error = new Failure();
        var result = Result<object>.CreateFail(error);

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
    public void Creating_Failed_Result_Of_T_With_Reason_Should_Succeed()
    {
        var reason = "Failure reason.";
        var result = Result<object>.CreateFail(reason);

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
    public void Creating_Failed_Result_Of_T_With_Exception_Should_Succeed()
    {
        var exception = new Exception();
        var result = Result<object>.CreateFail(exception);

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
        var result = Result<object>.CreateFail(reason, exception);

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
    public void Creating_Failed_Result_Of_T_With_Null_Failure_Should_Fail()
    {
        Failure failure = null!;
        Assert.Throws<ArgumentNullException>(() => Result<object>.CreateFail(failure));
    }
}