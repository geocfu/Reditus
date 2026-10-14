using System;
using Reditus.Abstractions;
using Reditus.Definitions;
using Xunit;

namespace Reditus.CompatibilityTests
{
    // Uses the library the way an application does, written in C# 7.3 so it compiles on every target.
    public class ConsumerScenarios
    {
        private sealed class NotFoundError : Error
        {
            public NotFoundError(string message)
                : base(message)
            {
            }
        }

        private static Result Save(bool succeed)
        {
            return succeed ? Result.Success() : Result.Failure(new Error("Could not save."));
        }

        private static Result<int> Divide(int dividend, int divisor)
        {
            if (divisor == 0)
            {
                return Result<int>.Failure(new Error("Cannot divide by zero."));
            }

            return Result<int>.Success(dividend / divisor);
        }

        private static Result<string> FindUser(int id)
        {
            if (id != 1)
            {
                return Result<string>.Failure(new NotFoundError("User " + id + " was not found."));
            }

            return Result<string>.Success("Alice");
        }

        private static Result<string> Describe(int dividend, int divisor)
        {
            var quotient = Divide(dividend, divisor);

            if (quotient.IsFailed)
            {
                return Result<string>.Failure(quotient);
            }

            return Result<string>.Success("Result: " + quotient.Value);
        }

        [Fact]
        public void Successful_Result_Reports_Success_And_Hides_Error()
        {
            var result = Save(true);

            Assert.True(result.IsSuccessful);
            Assert.False(result.IsFailed);
            Assert.Throws<InvalidOperationException>(() => result.Error);
        }

        [Fact]
        public void Failed_Result_Reports_Failure_And_Exposes_Error()
        {
            var result = Save(false);

            Assert.True(result.IsFailed);
            Assert.False(result.IsSuccessful);
            Assert.Equal("Could not save.", result.Error.Message);
            Assert.Null(result.Error.Exception);
        }

        [Fact]
        public void Failed_Result_Without_Error_Uses_Default_Message()
        {
            Assert.Equal("An error occurred.", Result.CreateFail().Error.Message);
            Assert.Equal("An error occurred.", Result<int>.CreateFail().Error.Message);
        }

        [Fact]
        public void Successful_Value_Result_Exposes_Value_And_Hides_Error()
        {
            var result = Divide(10, 2);

            Assert.True(result.IsSuccessful);
            Assert.Equal(5, result.Value);
            Assert.Throws<InvalidOperationException>(() => result.Error);
        }

        [Fact]
        public void Failed_Value_Result_Exposes_Error_And_Hides_Value()
        {
            var result = Divide(10, 0);

            Assert.True(result.IsFailed);
            Assert.Equal("Cannot divide by zero.", result.Error.Message);
            Assert.Throws<InvalidOperationException>(() => result.Value);
        }

        [Fact]
        public void Failure_Passed_Up_The_Call_Chain_Keeps_The_Same_Error()
        {
            var inner = Divide(1, 0);
            var outer = Describe(1, 0);

            Assert.True(outer.IsFailed);
            Assert.Equal(inner.Error.Message, outer.Error.Message);
            Assert.Equal("Result: 5", Describe(10, 2).Value);
        }

        [Fact]
        public void Failure_Copied_Between_Results_Is_The_Same_Error_Instance()
        {
            var failed = Result<int>.Failure(new Error("Original."));

            Assert.Same(failed.Error, Result<string>.Failure(failed).Error);
            Assert.Same(failed.Error, Result.Failure(Result.Failure(failed.Error)).Error);
        }

        [Fact]
        public void Copying_A_Failure_From_A_Successful_Result_Throws()
        {
            Assert.Throws<InvalidOperationException>(() => Result.Failure(Result.Success()));
            Assert.Throws<InvalidOperationException>(() => Result<string>.Failure(Result<int>.Success(1)));
        }

        [Fact]
        public void Custom_Error_Type_Can_Be_Matched_By_The_Caller()
        {
            var result = FindUser(2);

            Assert.True(result.IsFailed);
            Assert.IsType<NotFoundError>(result.Error);
            Assert.Equal("User 2 was not found.", result.Error.Message);
            Assert.Equal("Alice", FindUser(1).Value);
        }

        [Fact]
        public void Error_Keeps_Message_And_Exception()
        {
            var exception = new TimeoutException("Gateway timed out.");

            var withBoth = new Error("Payment was declined.", exception);
            var withBothReversed = new Error(exception, "Payment was declined.");
            var withException = new Error(exception);

            Assert.Equal("Payment was declined.", withBoth.Message);
            Assert.Same(exception, withBoth.Exception);
            Assert.Equal("Payment was declined.", withBothReversed.Message);
            Assert.Same(exception, withBothReversed.Exception);
            Assert.Equal("An error occurred.", withException.Message);
            Assert.Same(exception, withException.Exception);
        }

        [Fact]
        public void Null_Or_Empty_Inputs_Are_Rejected()
        {
            Assert.Throws<ArgumentNullException>(() => Result<string>.CreateSuccess(null));
            Assert.Throws<ArgumentNullException>(() => Result.CreateFail((Error)null));
            Assert.Throws<ArgumentNullException>(() => Result<int>.CreateFail((Error)null));
            Assert.Throws<ArgumentNullException>(() => Result.CreateFail((Result)null));
            Assert.Throws<ArgumentNullException>(() => Result<int>.CreateFail((Result<string>)null));
            Assert.Throws<ArgumentNullException>(() => new Error((string)null));
            Assert.Throws<ArgumentException>(() => new Error(" "));
            Assert.Throws<ArgumentNullException>(() => new Error((Exception)null));
        }

        [Fact]
        public void Results_Can_Be_Used_Through_Their_Interfaces()
        {
            IResult result = Save(false);
            IResult<object> covariant = FindUser(1);
            IError error = result.Error;

            Assert.True(result.IsFailed);
            Assert.Equal("Alice", covariant.Value);
            Assert.Equal("Could not save.", error.Message);
        }
    }
}
