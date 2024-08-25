using System.Net;
using Reditus.Extensions.AspNetCore.Http.Definitions;
using Xunit;

namespace Reditus.Extensions.AspNetCore.Http.UnitTests;

public class HttpAcceptedTests
{
    [Fact]
    public void Creating_HttpAccepted_Result_Should_Succeed()
    {
        var value = new object();
        var result = HttpAccepted<object>.CreateHttpResult(value);

        Assert.Multiple(() =>
        {
            Assert.True(result.IsSuccessful);
            Assert.Equal(HttpStatusCode.Accepted, result.Success.StatusCode);
            Assert.Equal(value, result.Success.Value);
        });
    }
}