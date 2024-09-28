using System;
using Reditus.Definitions;

namespace Reditus.Abstractions
{
    /// <summary>
    /// Describes a Result object.
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// Gets the Failure, if any, attached to the Result.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when Result is Successful.</exception>
        Error Error { get; }

        /// <summary>
        /// Gets a value indicating whether a Result is Successful.
        /// </summary>
        bool IsSuccessful { get; }

        /// <summary>
        /// Gets a value indicating whether a Result is Failed.
        /// </summary>
        bool IsFailed { get; }
    }
}