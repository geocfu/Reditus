using System;
using Reditus.Abstractions;

namespace Reditus.Definitions
{
    /// <summary>
    /// Represents a Failure object that encapsulates an error message and an optional exception.
    /// Implements the <see cref="IError"/> interface.
    /// </summary>
    public class Error : IError
    {
        /// <summary>
        /// Backing field for the Message property.
        /// </summary>
        private string _message = "An error occured.";

        /// <inheritdoc />
        public string Message
        {
            get
            {
                return _message;
            }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value), "The Message property must have a value.");
                }

                _message = value;
            }
        }

        /// <summary>
        /// Backing field for the Exception property.
        /// </summary>
        private Exception _exception;

        /// <inheritdoc />
        public Exception Exception
        {
            get
            {
                return _exception;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "The Exception property must have a value.");
                }

                _exception = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class with default values.
        /// The Message property will have its default value of "An error occurred.".
        /// </summary>
        public Error()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class with a specified message.
        /// The Exception property will be initialized to null.
        /// </summary>
        /// <param name="message">The message that describes the Failure.</param>
        /// <exception cref="ArgumentNullException">Thrown if the provided message is null.</exception>
        public Error(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class with a specified exception.
        /// The Message property will have its default value of "An error occurred.".
        /// </summary>
        /// <param name="exception">The exception to attach to the Failure.</param>
        /// <exception cref="ArgumentNullException">Thrown if the provided exception is null.</exception>
        public Error(Exception exception)
        {
            Exception = exception;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class with a specified message and exception.
        /// </summary>
        /// <param name="message">The message that describes the Failure.</param>
        /// <param name="exception">The exception to attach to the Failure.</param>
        /// <exception cref="ArgumentNullException">Thrown if the provided message is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the provided exception is null.</exception>
        public Error(string message, Exception exception)
        {
            Message = message;
            Exception = exception;
        }
    }
}