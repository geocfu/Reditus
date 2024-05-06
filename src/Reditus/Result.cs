using System;
using Reditus.Abstractions;
using Reditus.Definitions;

namespace Reditus
{
    /// <summary>
    /// Represents a Result object.
    /// </summary>
    public sealed class Result
    {
        /// <summary>
        /// The internal state of the Result.
        /// </summary>
        private readonly State _state;

        /// <summary>
        /// The internal error, if any, of the Result.
        /// </summary>
        private readonly IError _error;

        /// <summary>
        /// Gets a value indicating whether Result is successful.
        /// </summary>
        public bool IsSuccessful => _state == State.Successful;

        /// <summary>
        /// Gets a value indicating whether Result is failed.
        /// </summary>
        public bool IsFailed => _state == State.Failed;

        /// <summary>
        /// Gets the Error, if any, attached to the Result.
        /// </summary>
        public IError Error
        {
            get
            {
                if (_state == State.Successful)
                {
                    throw new InvalidOperationException(
                        "Accessing the Error property of a successful Result is invalid.");
                }

                return _error;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        private Result()
        {
            _state = State.Successful;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="error">The error to attach to the Result.</param>
        /// <exception cref="ArgumentNullException">Thrown when error is null.</exception>
        private Result(IError error)
        {
            _state = State.Failed;

            if (error == null)
            {
                throw new ArgumentNullException(
                    nameof(error),
                    "The Error property of a failed Result cannot be null.");
            }

            _error = error;
        }

        /// <summary>
        /// Creates a successful Result.
        /// </summary>
        /// <returns>A successful Result instance.</returns>
        public static Result Success() => new Result();

        /// <summary>
        /// Creates a failed Result.
        /// </summary>
        /// <param name="error">The error of the result.</param>
        /// <returns>A failed Result instance.</returns>
        public static Result Fail(IError error) => new Result(error);
    }
}