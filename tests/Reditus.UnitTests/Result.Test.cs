namespace Reditus.UnitTests;

using Definitions;

public class ResultTest
{
    [Fact]
    public void Creating_Successful_Result_Should_Succeed()
    {
        var result = Result.Successful();

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
        var result = Result.Failed(error);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Equal("This is an error.", result.Error.Message);
            Assert.Empty(result.Error.Metadata);
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

        var result = Result.Failed(error);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Equal("This is an error.", result.Error.Message);
            Assert.Empty(result.Error.Metadata);
            Assert.NotNull(result.Error.Exception);
            Assert.False(result.IsSuccessful);
            Assert.True(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_With_Metadata_Should_Succeed()
    {
        var metadata = new Dictionary<string, object>()
        {
            { "key", "value" }
        };
        var error = new Error("This is an error.", metadata);

        var result = Result.Failed(error);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Equal("This is an error.", result.Error.Message);
            Assert.NotEmpty(result.Error.Metadata);
            Assert.Null(result.Error.Exception);
            Assert.False(result.IsSuccessful);
            Assert.True(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_With_Exception_With_Metadata_Should_Succeed()
    {
        var exception = new Exception();
        var metadata = new Dictionary<string, object>()
        {
            { "key", "value" }
        };
        var error = new Error("This is an error.", exception, metadata);

        var result = Result.Failed(error);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Equal("This is an error.", result.Error.Message);
            Assert.NotEmpty(result.Error.Metadata);
            Assert.NotNull(result.Error.Exception);
            Assert.False(result.IsSuccessful);
            Assert.True(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_With_Null_Error_Should_Fail()
    {
        Assert.Throws<ArgumentNullException>(() => Result.Failed(null!));
    }

    [Fact]
    public void Converting_Failed_Result_Of_T_To_Failed_Result_Of_TY_Should_Succeed()
    {
        var resultOfT = Result<string>.Failed(new Error("This is an error."));
        var resultOfTy = Result<int>.Failed(resultOfT);

        Assert.Multiple(() =>
        {
            Assert.NotNull(resultOfTy);
            Assert.NotNull(resultOfTy.Error);
            Assert.Equal("This is an error.", resultOfTy.Error.Message);
            Assert.Empty(resultOfTy.Error.Metadata);
            Assert.Null(resultOfTy.Error.Exception);
            Assert.False(resultOfTy.IsSuccessful);
            Assert.True(resultOfTy.IsFailed);
        });
    }

    [Fact]
    public void Creating_Successful_Result_Of_T_Should_Succeed()
    {
        var value = new object();
        var result = Result<object>.Successful(value);

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
        Assert.Throws<ArgumentNullException>(() => Result<object>.Successful(null!));
    }

    [Fact]
    public void Creating_Failed_Result_Of_T_And_Accessing_Value_Should_Fail()
    {
        var error = new Error("This is an error.");
        var result = Result<object>.Failed(error);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Throws<InvalidOperationException>(() => result.Value);
            Assert.Equal("This is an error.", result.Error.Message);
            Assert.Empty(result.Error.Metadata);
            Assert.Null(result.Error.Exception);
            Assert.False(result.IsSuccessful);
            Assert.True(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_Of_T_Should_Succeed()
    {
        var error = new Error("This is an error.");
        var result = Result<object>.Failed(error);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Equal("This is an error.", result.Error.Message);
            Assert.Empty(result.Error.Metadata);
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

        var result = Result<object>.Failed(error);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Equal("This is an error.", result.Error.Message);
            Assert.Empty(result.Error.Metadata);
            Assert.NotNull(result.Error.Exception);
            Assert.False(result.IsSuccessful);
            Assert.True(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_Of_T_With_Metadata_Should_Succeed()
    {
        var metadata = new Dictionary<string, object>()
        {
            { "key", "value" }
        };
        var error = new Error("This is an error.", metadata);

        var result = Result<object>.Failed(error);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Equal("This is an error.", result.Error.Message);
            Assert.NotEmpty(result.Error.Metadata);
            Assert.Null(result.Error.Exception);
            Assert.False(result.IsSuccessful);
            Assert.True(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_Of_T_With_Exception_With_Metadata_Should_Succeed()
    {
        var exception = new Exception();
        var metadata = new Dictionary<string, object>()
        {
            { "key", "value" }
        };
        var error = new Error("This is an error.", exception, metadata);

        var result = Result<object>.Failed(error);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Equal("This is an error.", result.Error.Message);
            Assert.NotEmpty(result.Error.Metadata);
            Assert.NotNull(result.Error.Exception);
            Assert.False(result.IsSuccessful);
            Assert.True(result.IsFailed);
        });
    }

    [Fact]
    public void Creating_Failed_Result_Of_T_With_Null_Error_Should_Fail()
    {
        Assert.Throws<ArgumentNullException>(() => Result<object>.Failed(null!));
    }
}