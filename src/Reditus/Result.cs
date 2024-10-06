using System;
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
        protected State State { get; }

        /// <summary>
        /// The internal failure, if any, of the Result.
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
                        "Accessing the Failure property of a successful Result is invalid.");
                }

                return _error;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(
                        nameof(value),
                        "The Failure property cannot be null.");
                }

                _error = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if message is null or empty.</exception>
        protected Result()
        {
            State = State.Successful;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="error">The failure to attach to the Result.</param>
        /// <exception cref="ArgumentNullException">Thrown when failure is null.</exception>
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
        /// Implicitly converts an <see cref="Error"/> into a failed <see cref="Result"/>.
        /// This allows a direct assignment of an <see cref="Error"/> to a <see cref="Result"/> without explicitly calling CreateFail(Error)">.
        /// </summary>
        /// <param name="error">The error to be wrapped in a failed <see cref="Result"/>.</param>
        /// <returns>A failed <see cref="Result"/> instance containing the provided error.</returns>
        public static implicit operator Result(Error error) => new Result(error);

        /// <summary>
        /// Creates a successful Result.
        /// </summary>
        /// <returns>A successful Result instance.</returns>
        public static Result CreateSuccess()
        {
            return new Result();
        }

        /// <summary>
        /// Creates a failed Result.
        /// </summary>
        /// <returns>A failed Result instance.</returns>
        public static Result CreateFail()
        {
            var failure = new Error();
            return CreateFail(failure);
        }

        /// <summary>
        /// Creates a failed Result.
        /// </summary>
        /// <param name="error">The failure object of the result.</param>
        /// <returns>A failed Result instance.</returns>
        public static Result CreateFail(Error error)
        {
            return new Result(error);
        }

        /// <summary>
        /// Creates a failed Result from a different failed Result.
        /// </summary>
        /// <param name="result">The Result to copy from.</param>
        /// <returns>A failed Result instance.</returns>
        public static Result CreateFail(Result result)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result), "Cannot convert null into a Result");
            }

            if (result.IsSuccessful)
            {
                throw new InvalidOperationException("Converting a Successful Result to a Failed Result is invalid.");
            }

            return CreateFail(result.Error);
        }
    }
}