using Reditus.Definitions;
using Xunit;

namespace Reditus.UnitTests;

public class OperationCanceledErrorTests
{
    [Fact]
    public void Creating_OperationCanceledError_Should_Succeed()
    {
        var error = new OperationCanceledError();

        Assert.Multiple(() =>
        {
            Assert.Equal("The operation was canceled.", error.Message);
            Assert.Null(error.Exception);
        });
    }

    [Fact]
    public void Creating_OperationCanceledError_With_Exception_Should_Succeed()
    {
        var exception = new OperationCanceledException();
        var error = new OperationCanceledError(exception);

        Assert.Multiple(() =>
        {
            Assert.Equal("The operation was canceled.", error.Message);
            Assert.Same(exception, error.Exception);
        });
    }

    [Fact]
    public void Creating_OperationCanceledError_With_Null_Exception_Should_Throw()
    {
        Assert.Throws<ArgumentNullException>(() => new OperationCanceledError(null!));
    }

    [Fact]
    public void OperationCanceledError_Passed_Up_The_Call_Chain_Should_Keep_Its_Type()
    {
        var inner = Result<int>.Failure(new OperationCanceledError());
        var outer = Result<string>.Failure(inner);

        Assert.IsType<OperationCanceledError>(outer.Error);
    }
}
