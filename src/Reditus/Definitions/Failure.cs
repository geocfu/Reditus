using System;
using Reditus.Abstractions;

namespace Reditus.Definitions
{
    /// <summary>
    /// A general Failure object.
    /// </summary>
    public class Failure : IFailure
    {
        /// <inheritdoc />
        public string Message { get; }

        /// <inheritdoc />
        public Exception Exception { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Failure"/> class.
        /// </summary>
        public Failure()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Failure"/> class.
        /// </summary>
        /// <param name="message">The message that describes the Failure.</param>
        /// <exception cref="ArgumentException">Thrown if message is null or empty.</exception>
        public Failure(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException("The Message property must have a value.", nameof(message));
            }

            Message = message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Failure"/> class.
        /// </summary>
        /// <param name="exception">The exception to attach to the Failure.</param>
        public Failure(Exception exception)
        {
            Exception = exception;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Failure"/> class.
        /// </summary>
        /// <param name="message">The message that describes the Failure.</param>
        /// <param name="exception">The exception to attach to the Failure.</param>
        public Failure(string message, Exception exception)
            : this(message)
        {
            Exception = exception;
        }
    }
}