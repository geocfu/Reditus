using Reditus.Definitions;

namespace Reditus.UnitTests;

public class ResultTest
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
    public void Creating_Failed_Result_Should_Succeed()
    {
        var error = new Error("This is an error.");
        var result = Result.Fail(error);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Equal("This is an error.", result.Error.Message);
            Assert.Null(result.Error.Exception);
            Assert.False(result.IsSuccessful);
            Assert.True(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_With_Exception_Should_Succeed()
    {
        var exception = new Exception();
        var error = new Error("This is an error.", exception);

        var result = Result.Fail(error);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Equal("This is an error.", result.Error.Message);
            Assert.NotNull(result.Error.Exception);
            Assert.False(result.IsSuccessful);
            Assert.True(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_With_Null_Error_Should_Fail()
    {
        Assert.Throws<ArgumentNullException>(() => Result.Fail(null!));
    }

    [Fact]
    public void Creating_Successful_Result_Of_T_Should_Succeed()
    {
        var value = new object();
        var result = Result<object>.Success(value);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.Equal(value, result.Value);
            Assert.Throws<InvalidOperationException>(() => result.Error);
            Assert.True(result.IsSuccessful);
            Assert.False(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Successful_Result_Of_T_With_Null_Value_Should_Fail()
    {
        Assert.Throws<ArgumentNullException>(() => Result<object>.Success(null!));
    }

    [Fact]
    public void Creating_Failed_Result_Of_T_And_Accessing_Value_Should_Fail()
    {
        var error = new Error("This is an error.");
        var result = Result<object>.Fail(error);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Throws<InvalidOperationException>(() => result.Value);
            Assert.Equal("This is an error.", result.Error.Message);
            Assert.Null(result.Error.Exception);
            Assert.False(result.IsSuccessful);
            Assert.True(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_Of_T_Should_Succeed()
    {
        var error = new Error("This is an error.");
        var result = Result<object>.Fail(error);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Equal("This is an error.", result.Error.Message);
            Assert.Null(result.Error.Exception);
            Assert.False(result.IsSuccessful);
            Assert.True(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_Of_T_With_Exception_Should_Succeed()
    {
        var exception = new Exception();
        var error = new Error("This is an error.", exception);

        var result = Result<object>.Fail(error);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Equal("This is an error.", result.Error.Message);
            Assert.NotNull(result.Error.Exception);
            Assert.False(result.IsSuccessful);
            Assert.True(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_Of_T_With_Null_Error_Should_Fail()
    {
        Assert.Throws<ArgumentNullException>(() => Result<object>.Fail(null!));
    }

    [Fact]
    public void Converting_Failed_Result_Of_T_To_Failed_Result_Of_TY_Should_Succeed()
    {
        var resultOfT = Result<string>.Fail(new Error("This is an error."));
        var resultOfTy = Result<int>.Fail(resultOfT);

        Assert.Multiple(() =>
        {
            Assert.NotNull(resultOfTy);
            Assert.NotNull(resultOfTy.Error);
            Assert.Equal("This is an error.", resultOfTy.Error.Message);
            Assert.Null(resultOfTy.Error.Exception);
            Assert.False(resultOfTy.IsSuccessful);
            Assert.True(resultOfTy.IsFailed);
        });
    }

    [Fact]
    public void Converting_Successful_Result_Of_T_To_Failed_Result_Of_TY_Should_Fail()
    {
        var resultOfT = Result<string>.Success(string.Empty);

        Assert.Throws<InvalidOperationException>(() => Result<int>.Fail(resultOfT));
    }
}