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

        /// <inheritdoc />
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

        /// <inheritdoc />
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
        /// <exception cref="ArgumentNullException">Thrown if message is null or empty.</exception>
        protected Result(ISuccess success)
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
        protected Result(IFailure failure)
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

        /// <inheritdoc />
        public bool IsSuccessful => _state == State.Successful;

        /// <inheritdoc />
        public bool IsFailed => _state == State.Failed;

        /// <summary>
        /// Creates a successful Result.
        /// </summary>
        /// <returns>A successful Result instance.</returns>
        public static Result CreateSuccess()
        {
            var success = new Success();
            return CreateSuccess(success);
        }

        /// <summary>
        /// Creates a successful Result.
        /// </summary>
        /// <param name="success">The success object of the result.</param>
        /// <returns>A successful Result instance.</returns>
        public static Result CreateSuccess(ISuccess success)
        {
            return new Result(success);
        }

        /// <summary>
        /// Creates a successful Result from a different successful Result.
        /// </summary>
        /// <param name="result">The Result to copy from.</param>
        /// <returns>A failed Result instance.</returns>
        public static Result CreateSuccess(Result result)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result), "Cannot convert null into a Result");
            }

            if (result.IsFailed)
            {
                throw new InvalidOperationException("Converting a Failed Result to a Successful Result is invalid.");
            }

            return CreateSuccess(result.Success);
        }

        /// <summary>
        /// Creates a failed Result.
        /// </summary>
        /// <returns>A failed Result instance.</returns>
        public static Result CreateFail()
        {
            var failure = new Failure();
            return CreateFail(failure);
        }

        /// <summary>
        /// Creates a failed Result.
        /// </summary>
        /// <param name="failure">The failure object of the result.</param>
        /// <returns>A failed Result instance.</returns>
        public static Result CreateFail(IFailure failure)
        {
            return new Result(failure);
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

            return CreateFail(result.Failure);
        }
    }
}