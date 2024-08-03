using System;
using Reditus.Abstractions;
using Reditus.Definitions;

namespace Reditus.Factories
{
    /// <summary>
    /// Default factory.
    /// </summary>
    public static class Result
    {
        /// <summary>
        /// Creates a successful Result.
        /// </summary>
        /// <returns>A successful Result instance.</returns>
        public static Core.Result CreateSuccess()
        {
            var success = new Success();
            return new Core.Result(success);
        }

        /// <summary>
        /// Creates a successful Result.
        /// </summary>
        /// <param name="success">The success object of the result.</param>
        /// <returns>A successful Result instance.</returns>
        public static Core.Result CreateSuccess(ISuccess success)
        {
            return new Core.Result(success);
        }

        /// <summary>
        /// Creates a failed Result.
        /// </summary>
        /// <returns>A failed Result instance.</returns>
        public static Core.Result CreateFail()
        {
            var failure = new Failure();
            return new Core.Result(failure);
        }

        /// <summary>
        /// Creates a failed Result.
        /// </summary>
        /// <param name="failureMessage">The failure message of the result.</param>
        /// <returns>A failed Result instance.</returns>
        public static Core.Result CreateFail(string failureMessage)
        {
            var failure = new Failure(failureMessage);
            return new Core.Result(failure);
        }

        /// <summary>
        /// Creates a failed Result.
        /// </summary>
        /// <param name="exception">The failure exception of the result.</param>
        /// <returns>A failed Result instance.</returns>
        public static Core.Result CreateFail(Exception exception)
        {
            var failure = new Failure(exception);
            return new Core.Result(failure);
        }

        /// <summary>
        /// Creates a failed Result.
        /// </summary>
        /// <param name="failureMessage">The failure message of the result.</param>
        /// <param name="exception">The failure exception of the result.</param>
        /// <returns>A failed Result instance.</returns>
        public static Core.Result CreateFail(string failureMessage, Exception exception)
        {
            var failure = new Failure(failureMessage, exception);
            return new Core.Result(failure);
        }

        /// <summary>
        /// Creates a failed Result.
        /// </summary>
        /// <param name="failure">The failure object of the result.</param>
        /// <returns>A failed Result instance.</returns>
        public static Core.Result CreateFail(IFailure failure)
        {
            return new Core.Result(failure);
        }
    }
}