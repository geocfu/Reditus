using System;
using System.Collections.Generic;

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

        /// <summary>
        /// Gets the metadata, if any, that was attached to the Error.
        /// </summary>
        IReadOnlyDictionary<string, object> Metadata { get; }
    }
}