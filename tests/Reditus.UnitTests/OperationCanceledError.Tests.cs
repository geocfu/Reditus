using Reditus.Definitions;
using Xunit;

namespace Reditus.UnitTests;

public class OperationCanceledErrorTests
{
    [Fact]
    public void Creating_OperationCanceledError_Should_Have_Default_Message()
    {
        var error = new OperationCanceledError();

        Assert.Multiple(() =>
        {
            Assert.Equal("The operation was canceled.", error.Message);
            Assert.Null(error.Exception);
        });
    }

    [Fact]
    public void Creating_OperationCanceledError_With_Message_Should_Succeed()
    {
        var error = new OperationCanceledError("Error reason.");

        Assert.Multiple(() =>
        {
            Assert.Equal("Error reason.", error.Message);
            Assert.Null(error.Exception);
        });
    }

    [Fact]
    public void Creating_OperationCanceledError_With_Exception_Should_Have_Default_Message()
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
    public void Creating_OperationCanceledError_With_Message_And_Exception_Should_Succeed()
    {
        var exception = new OperationCanceledException();
        var error = new OperationCanceledError("Error reason.", exception);

        Assert.Multiple(() =>
        {
            Assert.Equal("Error reason.", error.Message);
            Assert.Same(exception, error.Exception);
        });
    }

    [Fact]
    public void Creating_OperationCanceledError_With_Exception_And_Message_Should_Succeed()
    {
        var exception = new OperationCanceledException();
        var error = new OperationCanceledError(exception, "Error reason.");

        Assert.Multiple(() =>
        {
            Assert.Equal("Error reason.", error.Message);
            Assert.Same(exception, error.Exception);
        });
    }

    [Fact]
    public void Creating_OperationCanceledError_With_Null_Message_Should_Fail()
    {
        string message = null!;

        Assert.Multiple(() =>
        {
            Assert.Throws<ArgumentNullException>(() => new OperationCanceledError(message));
            Assert.Throws<ArgumentNullException>(() => new OperationCanceledError(message, new Exception()));
            Assert.Throws<ArgumentNullException>(() => new OperationCanceledError(new Exception(), message));
        });
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Creating_OperationCanceledError_With_Empty_Or_Whitespace_Message_Should_Fail(string message)
    {
        Assert.Multiple(() =>
        {
            Assert.Throws<ArgumentException>(() => new OperationCanceledError(message));
            Assert.Throws<ArgumentException>(() => new OperationCanceledError(message, new Exception()));
            Assert.Throws<ArgumentException>(() => new OperationCanceledError(new Exception(), message));
        });
    }

    [Fact]
    public void Creating_OperationCanceledError_With_Null_Exception_Should_Fail()
    {
        Exception exception = null!;

        Assert.Multiple(() =>
        {
            Assert.Throws<ArgumentNullException>(() => new OperationCanceledError(exception));
            Assert.Throws<ArgumentNullException>(() => new OperationCanceledError("Error reason.", exception));
            Assert.Throws<ArgumentNullException>(() => new OperationCanceledError(exception, "Error reason."));
        });
    }

    [Fact]
    public void OperationCanceledError_Passed_Up_The_Call_Chain_Should_Keep_Its_Type()
    {
        var inner = Result<int>.Failure(new OperationCanceledError());
        var outer = Result<string>.Failure(inner);

        Assert.IsType<OperationCanceledError>(outer.Error);
    }
}
