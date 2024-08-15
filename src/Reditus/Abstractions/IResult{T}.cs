using System;

namespace Reditus.Abstractions
{
    /// <summary>
    /// Describes a Result{T} object.
    /// </summary>
    /// <typeparam name="T">The type contained in <see cref="T:Result{T}" />.</typeparam>
    public interface IResult<out T> : IResult
    {
        /// <summary>
        /// Gets the Success, if any, attached to the Result.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when Result is Failed.</exception>
        new ISuccess<T> Success { get; }

        // /// <summary>
        // /// Gets the Failure, if any, attached to the Result.
        // /// </summary>
        // /// <exception cref="InvalidOperationException">Thrown when Result is Successful.</exception>
        // IFailure Failure { get; }
        //
        // /// <summary>
        // /// Gets a value indicating whether a Result is Successful.
        // /// </summary>
        // bool IsSuccessful { get; }
        //
        // /// <summary>
        // /// Gets a value indicating whether a Result is Failed.
        // /// </summary>
        // bool IsFailed { get; }
    }
}