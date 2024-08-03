using System;

namespace Reditus.Abstractions
{
    /// <summary>
    /// Describes a Failure object.
    /// </summary>
    public interface IFailure
    {
        /// <summary>
        /// Gets the message, if any, that describes the Failure.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Gets the exception, if any, that was attached to the Failure.
        /// </summary>
        Exception Exception { get; }
    }
}