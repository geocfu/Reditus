using Xunit;

namespace Reditus.Extensions.AspNetCore.UnitTests;

public class HttpResultTests
{
    [Fact]
    public void Creating_Successful_Result_Should_Succeed()
    {
        var result = HttpResult.CreateNoContent();
        
        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.Throws<InvalidOperationException>(() => result.Failure);
            Assert.True(result.IsSuccessful);
            Assert.False(result.IsFailed);
            // Assert.Equal(HttpStatusCode.NoContent, (IHttpSuccess)result.Success);
        });
    }
}