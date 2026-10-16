using Reditus.Definitions;
using Xunit;

namespace Reditus.UnitTests;

public class ErrorTests
{
    [Fact]
    public void Creating_Error_Should_Have_Default_Message()
    {
        var error = new Error();

        Assert.Multiple(() =>
        {
            Assert.Equal("An error occurred.", error.Message);
            Assert.Null(error.Exception);
        });
    }

    [Fact]
    public void Creating_Error_With_Message_Should_Succeed()
    {
        var error = new Error("Error reason.");

        Assert.Multiple(() =>
        {
            Assert.Equal("Error reason.", error.Message);
            Assert.Null(error.Exception);
        });
    }

    [Fact]
    public void Creating_Error_With_Exception_Should_Have_Default_Message()
    {
        var exception = new Exception();
        var error = new Error(exception);

        Assert.Multiple(() =>
        {
            Assert.Equal("An error occurred.", error.Message);
            Assert.Same(exception, error.Exception);
        });
    }

    [Fact]
    public void Creating_Error_With_Message_And_Exception_Should_Succeed()
    {
        var exception = new Exception();
        var error = new Error("Error reason.", exception);

        Assert.Multiple(() =>
        {
            Assert.Equal("Error reason.", error.Message);
            Assert.Same(exception, error.Exception);
        });
    }

    [Fact]
    public void Creating_Error_With_Exception_And_Message_Should_Succeed()
    {
        var exception = new Exception();
        var error = new Error(exception, "Error reason.");

        Assert.Multiple(() =>
        {
            Assert.Equal("Error reason.", error.Message);
            Assert.Same(exception, error.Exception);
        });
    }

    [Fact]
    public void Creating_Error_With_Null_Message_Should_Fail()
    {
        string message = null!;

        Assert.Multiple(() =>
        {
            Assert.Throws<ArgumentNullException>(() => new Error(message));
            Assert.Throws<ArgumentNullException>(() => new Error(message, new Exception()));
            Assert.Throws<ArgumentNullException>(() => new Error(new Exception(), message));
        });
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Creating_Error_With_Empty_Or_Whitespace_Message_Should_Fail(string message)
    {
        Assert.Multiple(() =>
        {
            Assert.Throws<ArgumentException>(() => new Error(message));
            Assert.Throws<ArgumentException>(() => new Error(message, new Exception()));
            Assert.Throws<ArgumentException>(() => new Error(new Exception(), message));
        });
    }

    [Fact]
    public void Creating_Error_With_Null_Exception_Should_Fail()
    {
        Exception exception = null!;

        Assert.Multiple(() =>
        {
            Assert.Throws<ArgumentNullException>(() => new Error(exception));
            Assert.Throws<ArgumentNullException>(() => new Error("Error reason.", exception));
            Assert.Throws<ArgumentNullException>(() => new Error(exception, "Error reason."));
        });
    }
}
