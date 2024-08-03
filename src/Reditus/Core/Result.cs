using System;
using Reditus.Abstractions;
using Reditus.Definitions;

namespace Reditus.Core
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
        /// The internal failure, if any, of the Result.
        /// </summary>
        private readonly IFailure _failure;

        /// <summary>
        /// The internal success, if any, of the Result.
        /// </summary>
        private readonly ISuccess _success;

        /// <summary>
        /// Gets a value indicating whether a Result is successful.
        /// </summary>
        public bool IsSuccessful => _state == State.Successful;

        /// <summary>
        /// Gets a value indicating whether a Result is failed.
        /// </summary>
        public bool IsFailed => _state == State.Failed;

        /// <summary>
        /// Gets the Success, if any, attached to the Result.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when Result is Failed.</exception>
        /// <exception cref="ArgumentNullException">Thrown when Result is Successful.</exception>
        public ISuccess Success
        {
            get
            {
                if (_state == State.Failed)
                {
                    throw new InvalidOperationException(
                        "Accessing the Success property of a failed Result is invalid.");
                }

                return _success;
            }
        }

        /// <summary>
        /// Gets the Failure, if any, attached to the Result.
        /// </summary>
        public IFailure Failure
        {
            get
            {
                if (_state == State.Successful)
                {
                    throw new InvalidOperationException(
                        "Accessing the Failure property of a successful Result is invalid.");
                }

                return _failure;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="success">The success object.</param>
        public Result(ISuccess success)
        {
            _state = State.Successful;

            if (success == null)
            {
                throw new ArgumentNullException(
                    nameof(success),
                    "The Success object of a successful Result cannot be null.");
            }

            _success = success;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="failure">The failure to attach to the Result.</param>
        /// <exception cref="ArgumentNullException">Thrown when failure is null.</exception>
        public Result(IFailure failure)
        {
            _state = State.Failed;

            if (failure == null)
            {
                throw new ArgumentNullException(
                    nameof(failure),
                    "The Failure object of a failed Result cannot be null.");
            }

            _failure = failure;
        }
    }
}