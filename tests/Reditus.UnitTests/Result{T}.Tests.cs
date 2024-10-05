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
            Assert.Throws<InvalidOperationException>(() => result.Error);
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
            Assert.NotNull(result.Error);
            Assert.Equal("An error occured.", result.Error.Message);
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
        var result = Result<object>.CreateFail(failure);

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
        var result = Result<object>.CreateFail(failure);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Equal("An error occured.", result.Error.Message);
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
        var result = Result<object>.CreateFail(failure);

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
        var result = Result<object>.CreateFail(error);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Equal("An error occured.", result.Error.Message);
            Assert.Null(result.Error.Exception);
            Assert.False(result.IsSuccessful);
            Assert.True(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_Of_T_With_Null_Error_Should_Fail()
    {
        Error error = null!;
        Assert.Throws<ArgumentNullException>(() => Result<object>.CreateFail(error));
    }

    [Fact]
    public void Creating_Failed_Result_Of_T_From_A_Different_Failed_Result_Of_Y_Should_Succeed()
    {
        var oldResult = Result<int>.CreateFail();
        var newResult = Result<bool>.CreateFail(oldResult);

        Assert.Multiple(() =>
        {
            Assert.NotNull(newResult);
            Assert.NotNull(newResult.Error);
            Assert.Equal("An error occured.", newResult.Error.Message);
            Assert.Null(newResult.Error.Exception);
            Assert.False(newResult.IsSuccessful);
            Assert.True(newResult.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_Of_T_From_Null_Result_Should_Fail()
    {
        Result<object> oldResult = null!;
        Assert.Throws<ArgumentNullException>(() => Result<object>.CreateFail(oldResult));
    }
}