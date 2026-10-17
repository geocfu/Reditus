using System;

namespace Reditus.Definitions
{
    /// <summary>
    /// Represents an Error caused by a canceled operation, for example a cancellation requested through a
    /// <see cref="System.Threading.CancellationToken"/>. Lets callers tell a cancellation apart from a failure.
    /// </summary>
    public class OperationCanceledError : Error
    {
        /// <summary>
        /// The default value of the Message property.
        /// </summary>
        private const string DefaultMessage = "The operation was canceled.";

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationCanceledError"/> class.
        /// The Message property will have its default value of "The operation was canceled.".
        /// The Exception property will be initialized to null.
        /// </summary>
        public OperationCanceledError()
            : base(DefaultMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationCanceledError"/> class with a specified message.
        /// The Exception property will be initialized to null.
        /// </summary>
        /// <param name="message">The message that describes the Error.</param>
        /// <exception cref="ArgumentNullException">Thrown if the provided message is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the provided message is empty or whitespace.</exception>
        public OperationCanceledError(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationCanceledError"/> class with a specified exception.
        /// The Message property will have its default value of "The operation was canceled.".
        /// </summary>
        /// <param name="exception">The exception to attach to the Error.</param>
        /// <exception cref="ArgumentNullException">Thrown if the provided exception is null.</exception>
        public OperationCanceledError(Exception exception)
            : base(DefaultMessage, exception)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationCanceledError"/> class with a specified message and
        /// exception.
        /// </summary>
        /// <param name="message">The message that describes the Error.</param>
        /// <param name="exception">The exception to attach to the Error.</param>
        /// <exception cref="ArgumentNullException">Thrown if the provided message is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the provided message is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the provided exception is null.</exception>
        public OperationCanceledError(string message, Exception exception)
            : base(message, exception)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationCanceledError"/> class with a specified message and
        /// exception.
        /// </summary>
        /// <param name="exception">The exception to attach to the Error.</param>
        /// <param name="message">The message that describes the Error.</param>
        /// <exception cref="ArgumentNullException">Thrown if the provided exception is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the provided message is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the provided message is empty or whitespace.</exception>
        public OperationCanceledError(Exception exception, string message)
            : base(exception, message)
        {
        }
    }
}
