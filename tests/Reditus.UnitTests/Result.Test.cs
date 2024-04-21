using Reditus.Definitions.Errors;

namespace Reditus.UnitTests;

public class ResultTest
{
    [Fact]
    public void Creating_Successful_Result_Should_Succeed()
    {
        var result = Result.Successful();

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.Null(result.Error);
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
            Assert.False(result.IsSuccessful);
            Assert.True(result.IsFailed);
        });
    }
}