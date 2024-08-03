using Reditus.Factories;
using Reditus.Definitions;

namespace Reditus.UnitTests;

public class ResultTest
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

    // apo edo kai kato den ta exo ftiaksei

    [Fact]
    public void Creating_Successful_Result_Of_T_Should_Succeed()
    {
        var value = new object();
        var result = Result<object>.CreateSuccess(value);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Success.Value);
            Assert.Throws<InvalidOperationException>(() => result.Failure);
            Assert.True(result.IsSuccessful);
            Assert.False(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Successful_Result_Of_T_With_ISuccess_Should_Succeed()
    {
        var value = new object();
        var success = new Success<object>(value);
        var result = Result<object>.CreateSuccess(success);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Success.Value);
            Assert.Throws<InvalidOperationException>(() => result.Failure);
            Assert.True(result.IsSuccessful);
            Assert.False(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_ResultOf_T__Should_Succeed()
    {
        var result = Result<object>.CreateFail();

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
    public void Creating_Failed_Result_Of_T_With_Reason_Should_Succeed()
    {
        var reason = "Failure reason.";
        var result = Result<object>.CreateFail(reason);

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
    public void Creating_Failed_Result_Of_T_With_Exception_Should_Succeed()
    {
        var exception = new Exception();
        var result = Result.CreateFail(exception);

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
    public void Creating_Failed_Result_Of_T_With_Reason_And_Exception_Should_Succeed()
    {
        var reason = "Failure reason.";
        var exception = new Exception();
        var result = Result<object>.CreateFail(reason, exception);

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
    public void Creating_Failed_Result_Of_TWith_IFailure_Should_Succeed()
    {
        var error = new Failure();
        var result = Result<object>.CreateFail(error);

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
    public void Creating_Failed_Result_Of_T_With_Null_Failure_Should_Fail()
    {
        Failure failure = null!;
        Assert.Throws<ArgumentNullException>(() => Result<object>.CreateFail(failure));
    }

    [Fact]
    public void Converting_Failed_Result_Of_T_To_Failed_Result_Of_TY_Should_Succeed()
    {
        var resultOfT = Result<string>.CreateFail(new Failure());
        var resultOfTy = Result<int>.CreateFail(resultOfT);

        Assert.Multiple(() =>
        {
            Assert.NotNull(resultOfTy);
            Assert.NotNull(resultOfTy.Failure);
            Assert.Throws<InvalidOperationException>(() => resultOfTy.Success);
            Assert.Null(resultOfTy.Failure.Message);
            Assert.Null(resultOfTy.Failure.Exception);
            Assert.False(resultOfTy.IsSuccessful);
            Assert.True(resultOfTy.IsFailed);
        });
    }

    [Fact]
    public void Converting_Successful_Result_Of_T_To_Failed_Result_Of_TY_Should_Fail()
    {
        var resultOfT = Result<string>.CreateSuccess(string.Empty);

        Assert.Throws<InvalidOperationException>(() => Result<int>.CreateFail(resultOfT));
    }
}