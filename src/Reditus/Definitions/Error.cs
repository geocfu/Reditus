using System;
using Reditus.Abstractions;

namespace Reditus.Definitions
{
    /// <summary>
    /// A general Error object.
    /// </summary>
    public class Error : IError
    {
        /// <inheritdoc />
        public string Message { get; }

        /// <inheritdoc />
        public Exception Exception { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class.
        /// </summary>
        /// <param name="message">The message that describes the Error.</param>
        /// <exception cref="ArgumentException">Thrown if message is null or empty.</exception>
        public Error(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException("The Message property must have a value.", nameof(message));
            }

            Message = message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class.
        /// </summary>
        /// <param name="message">The message that describes the Error.</param>
        /// <param name="exception">The exception to attach to the Error.</param>
        public Error(string message, Exception exception)
            : this(message)
        {
            Exception = exception;
        }
    }
}