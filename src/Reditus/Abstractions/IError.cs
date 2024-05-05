using System;

namespace Reditus.Abstractions
{
    /// <summary>
    /// Describes an Error object.
    /// </summary>
    public interface IError
    {
        /// <summary>
        /// Gets the message that describes the Error.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Gets the exception, if any, that was attached to the Error.
        /// </summary>
        Exception Exception { get; }
    }
}