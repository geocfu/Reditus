using System;
using Reditus.Abstractions;
using Reditus.Definitions;

namespace Reditus
{
    /// <summary>
    /// Represents a Result{T} object.
    /// </summary>
    /// <typeparam name="T">The type contained in <see cref="T:Result{T}" />.</typeparam>
    public class Result<T> : Result, IResult<T>
    {
        /// <inheritdoc />
        public new ISuccess<T> Success => (ISuccess<T>)base.Success;

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class.
        /// </summary>
        /// <param name="success">The success of the Result.</param>
        /// <exception cref="ArgumentNullException">Thrown when success is null.</exception>
        protected Result(ISuccess<T> success)
            : base(success)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class.
        /// </summary>
        /// <param name="failure">The failure to attach to the Result.</param>
        /// <exception cref="ArgumentNullException">Thrown when failure is null.</exception>
        protected Result(IFailure failure)
            : base(failure)
        {
        }

        /// <summary>
        /// Creates a successful Result{T}.
        /// </summary>
        /// <param name="value">The value of a success object of the result.</param>
        /// <returns>A successful Result instance.</returns>
        public static Result<T> CreateSuccess(T value)
        {
            var success = new Success<T>(value);
            return CreateSuccess(success);
        }

        /// <summary>
        /// Creates a successful Result{T}.
        /// </summary>
        /// <param name="success">The success object of the result.</param>
        /// <returns>A successful Result instance.</returns>
        public static Result<T> CreateSuccess(ISuccess<T> success)
        {
            return new Result<T>(success);
        }

        /// <summary>
        /// Creates a failed Result{T}.
        /// </summary>
        /// <returns>A failed Result instance.</returns>
        public static new Result<T> CreateFail()
        {
            var failure = new Failure();
            return CreateFail(failure);
        }

        /// <summary>
        /// Creates a failed Result{T}.
        /// </summary>
        /// <param name="failure">The failure object of the result.</param>
        /// <returns>A failed Result instance.</returns>
        public static new Result<T> CreateFail(IFailure failure)
        {
            return new Result<T>(failure);
        }

        /// <summary>
        /// Creates a failed Result{T} from a failed Result{TY}.
        /// </summary>
        /// <param name="result">The Result to copy from.</param>
        /// <returns>A failed Result instance.</returns>
        /// <typeparam name="TY">The type of the Result to copy from.</typeparam>
        public static Result<T> CreateFail<TY>(Result<TY> result)
        {
            if (result.IsSuccessful)
            {
                throw new InvalidOperationException("Converting a Successful Result to a Failed Result is invalid.");
            }

            return new Result<T>(result.Failure);
        }
    }
}