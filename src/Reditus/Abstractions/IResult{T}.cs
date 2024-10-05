using System;

namespace Reditus.Abstractions
{
    /// <summary>
    /// Describes a Result{T} object.
    /// </summary>
    /// <typeparam name="T">The type contained in <see cref="Result{T}" />.</typeparam>
    public interface IResult<out T> : IResult
    {
        /// <summary>
        /// Gets the Success, if any, attached to the Result.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when Result is Failed.</exception>
        T Value { get; }
    }
}