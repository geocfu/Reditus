using System;
using Reditus.Abstractions;
using Reditus.Definitions;

namespace Reditus.Factories
{
    /// <summary>
    /// Represents a Result object.
    /// </summary>
    /// <typeparam name="T">The type contained by this <see cref="T:Result{T}" />.</typeparam>
    public static class Result<T>
    {
        /// <summary>
        /// Creates a successful Result{T}.
        /// </summary>
        /// <param name="value">The value of a success object of the result.</param>
        /// <returns>A successful Result instance.</returns>
        public static Core.Result<T> CreateSuccess(T value)
        {
            var success = new Success<T>(value);
            return new Core.Result<T>(success);
        }

        /// <summary>
        /// Creates a successful Result.
        /// </summary>
        /// <param name="success">The success object of the result.</param>
        /// <returns>A successful Result instance.</returns>
        public static Core.Result<T> CreateSuccess(ISuccess<T> success)
        {
            return new Core.Result<T>(success);
        }

        /// <summary>
        /// Creates a failed Result.
        /// </summary>
        /// <returns>A failed Result instance.</returns>
        public static Core.Result<T> CreateFail()
        {
            var failure = new Failure();
            return new Core.Result<T>(failure);
        }

        /// <summary>
        /// Creates a failed Result.
        /// </summary>
        /// <param name="failureMessage">The failure message of the result.</param>
        /// <returns>A failed Result instance.</returns>
        public static Core.Result<T> CreateFail(string failureMessage)
        {
            var failure = new Failure(failureMessage);
            return new Core.Result<T>(failure);
        }

        /// <summary>
        /// Creates a failed Result.
        /// </summary>
        /// <param name="exception">The failure exception of the result.</param>
        /// <returns>A failed Result instance.</returns>
        public static Core.Result<T> CreateFail(Exception exception)
        {
            var failure = new Failure("A failure has occured.", exception);
            return new Core.Result<T>(failure);
        }

        /// <summary>
        /// Creates a failed Result.
        /// </summary>
        /// <param name="failureMessage">The failure message of the result.</param>
        /// <param name="exception">The failure exception of the result.</param>
        /// <returns>A failed Result instance.</returns>
        public static Core.Result<T> CreateFail(string failureMessage, Exception exception)
        {
            var failure = new Failure(failureMessage, exception);
            return new Core.Result<T>(failure);
        }

        /// <summary>
        /// Creates a failed Result.
        /// </summary>
        /// <param name="failure">The failure object of the result.</param>
        /// <returns>A failed Result instance.</returns>
        public static Core.Result<T> CreateFail(IFailure failure)
        {
            return new Core.Result<T>(failure);
        }

        /// <summary>
        /// Creates a failed Result.
        /// </summary>
        /// <param name="result">The Result to copy from.</param>
        /// <returns>A failed Result instance.</returns>
        /// <typeparam name="TY">The type of the Result to copy from.</typeparam>
        public static Core.Result<T> CreateFail<TY>(Core.Result<TY> result)
        {
            if (result.IsSuccessful)
            {
                throw new InvalidOperationException("Converting a Successful Result to a Failed Result is invalid.");
            }

            return new Core.Result<T>(result.Failure);
        }
    }
}