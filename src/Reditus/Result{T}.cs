using System;
using Reditus.Abstractions;
using Reditus.Definitions;

namespace Reditus
{
    /// <summary>
    /// Represents a Result{T} object.
    /// </summary>
    /// <typeparam name="T">The type contained in <see cref="Result{T}" />.</typeparam>
    public class Result<T> : IResult<T>
    {
        /// <summary>
        /// Gets the internal state of the Result.
        /// </summary>
        private State State { get; }

        /// <summary>
        /// The backing field for the Value.
        /// </summary>
        private T _value;

        /// <inheritdoc />
        public T Value
        {
            get
            {
                if (State == State.Failed)
                {
                    throw new InvalidOperationException("Accessing the Value property of a failed Result is invalid.");
                }

                return _value;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(
                        nameof(value),
                        "The Value property cannot be null.");
                }

                _value = value;
            }
        }

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

        /// <inheritdoc />
        public bool IsSuccessful => State == State.Successful;

        /// <inheritdoc />
        public bool IsFailed => !IsSuccessful;

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class.
        /// </summary>
        /// <param name="value">The value of the Result.</param>
        /// <exception cref="ArgumentNullException">Thrown when value is null.</exception>
        protected Result(T value)
        {
            State = State.Successful;
            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class.
        /// </summary>
        /// <param name="error">The error to attach to the Result.</param>
        /// <exception cref="ArgumentNullException">Thrown when error is null.</exception>
        protected Result(Error error)
        {
            State = State.Failed;
            Error = error;
        }

        /// <summary>
        /// Creates a successful Result{T}.
        /// </summary>
        /// <param name="value">The value of the result.</param>
        /// <returns>A successful Result instance.</returns>
        public static Result<T> CreateSuccess(T value)
        {
            return new Result<T>(value);
        }

        /// <summary>
        /// Creates a failed Result{T}.
        /// </summary>
        /// <returns>A failed Result instance.</returns>
        public static Result<T> CreateFail()
        {
            var failure = new Error();
            return CreateFail(failure);
        }

        /// <summary>
        /// Creates a failed Result{T}.
        /// </summary>
        /// <param name="error">The error of the result.</param>
        /// <returns>A failed Result instance.</returns>
        public static Result<T> CreateFail(Error error)
        {
            return new Result<T>(error);
        }

        /// <summary>
        /// Creates a failed Result{T} from a failed Result{TY}.
        /// </summary>
        /// <param name="result">The Result to copy from.</param>
        /// <returns>A failed Result instance.</returns>
        /// <typeparam name="TY">The type of the Result to copy from.</typeparam>
        public static Result<T> CreateFail<TY>(Result<TY> result)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result), "Cannot convert null into a Result");
            }

            if (result.IsSuccessful)
            {
                throw new InvalidOperationException("Converting a Successful Result to a Failed Result is invalid.");
            }

            return new Result<T>(result.Error);
        }
    }
}