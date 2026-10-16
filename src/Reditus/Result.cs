using System;
using System.Threading;
using Reditus.Abstractions;
using Reditus.Definitions;

namespace Reditus
{
    /// <summary>
    /// Represents a Result object.
    /// </summary>
    public class Result : IResult
    {
        /// <summary>
        /// Gets the internal state of the Result.
        /// </summary>
        private State State { get; }

        /// <summary>
        /// The internal error, if any, of the Result.
        /// </summary>
        private Error _error;

        /// <inheritdoc />
        public Error Error
        {
            get
            {
                if (State == State.Successful)
                {
                    throw new InvalidOperationException(
                        "Accessing the Error property of a successful Result is invalid.");
                }

                return _error;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(
                        nameof(value),
                        "The Error property cannot be null.");
                }

                _error = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        protected Result()
        {
            State = State.Successful;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="error">The error to attach to the Result.</param>
        /// <exception cref="ArgumentNullException">Thrown when error is null.</exception>
        protected Result(Error error)
        {
            State = State.Failed;
            Error = error;
        }

        /// <inheritdoc />
        public bool IsSuccessful => State == State.Successful;

        /// <inheritdoc />
        public bool IsFailed => !IsSuccessful;

        /// <summary>
        /// Creates a successful Result.
        /// </summary>
        /// <returns>A successful Result instance.</returns>
        public static Result Success()
        {
            return new Result();
        }

        /// <summary>
        /// Creates a failed Result.
        /// </summary>
        /// <returns>A failed Result instance.</returns>
        public static Result Failure()
        {
            var failure = new Error();
            return Failure(failure);
        }

        /// <summary>
        /// Creates a failed Result.
        /// </summary>
        /// <param name="error">The error of the result.</param>
        /// <returns>A failed Result instance.</returns>
        public static Result Failure(Error error)
        {
            return new Result(error);
        }

        /// <summary>
        /// Creates a failed Result. The Error is an <see cref="OperationCanceledError"/> if the provided token was
        /// canceled; otherwise, the provided error.
        /// </summary>
        /// <param name="error">The error of the result if the token was not canceled.</param>
        /// <param name="cancellationToken">The token of the operation that failed.</param>
        /// <returns>A failed Result instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown when error is null.</exception>
        public static Result Failure(Error error, CancellationToken cancellationToken)
        {
            return new Result(OperationCanceledErrorHelpers.FromCancellation(error, cancellationToken));
        }

        /// <summary>
        /// Creates a failed Result from a caught exception. The Error is an <see cref="OperationCanceledError"/>
        /// if the exception was caused by the cancellation of the provided token; otherwise, an <see cref="Error"/>.
        /// </summary>
        /// <param name="exception">The caught exception.</param>
        /// <param name="cancellationToken">The token of the operation that threw the exception.</param>
        /// <returns>A failed Result instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown when exception is null.</exception>
        public static Result Failure(Exception exception, CancellationToken cancellationToken)
        {
            return new Result(OperationCanceledErrorHelpers.FromException(exception, cancellationToken));
        }

        /// <summary>
        /// Creates a failed Result from a different failed Result.
        /// </summary>
        /// <param name="result">The Result to copy from.</param>
        /// <returns>A failed Result instance.</returns>
        public static Result Failure(Result result)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result), "Cannot convert null into a Result");
            }

            if (result.IsSuccessful)
            {
                throw new InvalidOperationException("Converting a Successful Result to a Failed Result is invalid.");
            }

            return Failure(result.Error);
        }
    }
}