using System;
using Reditus.Abstractions;
using Reditus.Definitions;

namespace Reditus
{
    /// <summary>
    /// Represents a Result{T} object.
    /// </summary>
    /// <typeparam name="T">The type contained in <see cref="Result{T}" />.</typeparam>
    public class Result<T> : Result, IResult<T>
    {
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
                    throw new InvalidOperationException(
                        "Accessing the Value property of a failed Result is invalid.");
                }

                return _value;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(
                        nameof(value),
                        "The Success property cannot be null.");
                }

                _value = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class.
        /// </summary>
        /// <param name="value">The success of the Result.</param>
        /// <exception cref="ArgumentNullException">Thrown when success is null.</exception>
        protected Result(T value)
        {
            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class.
        /// </summary>
        /// <param name="error">The failure to attach to the Result.</param>
        /// <exception cref="ArgumentNullException">Thrown when failure is null.</exception>
        protected Result(Error error)
            : base(error)
        {
        }

        /// <summary>
        /// Creates a successful Result{T}.
        /// </summary>
        /// <param name="value">The success object of the result.</param>
        /// <returns>A successful Result instance.</returns>
        public static Result<T> CreateSuccess(T value)
        {
            return new Result<T>(value);
        }

        /// <summary>
        /// Creates a failed Result{T}.
        /// </summary>
        /// <returns>A failed Result instance.</returns>
        public static new Result<T> CreateFail()
        {
            var failure = new Error();
            return CreateFail(failure);
        }

        /// <summary>
        /// Creates a failed Result{T}.
        /// </summary>
        /// <param name="error">The failure object of the result.</param>
        /// <returns>A failed Result instance.</returns>
        public static new Result<T> CreateFail(Error error)
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