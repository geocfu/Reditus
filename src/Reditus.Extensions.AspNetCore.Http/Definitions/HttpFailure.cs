using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using Reditus.Definitions;
using Reditus.Extensions.AspNetCore.Http.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Definitions
{
    /// <summary>
    /// Represents an HTTP-specific failure response, encapsulating an HTTP status code and validation errors.
    /// This class implements the <see cref="IHttpFailure"/> interface and extends the <see cref="Failure"/> class.
    /// </summary>
    public class HttpFailure : Failure, IHttpFailure
    {
        /// <summary>
        /// Gets the HTTP status code associated with the failure.
        /// Defaults to <see cref="HttpStatusCode.InternalServerError"/> if not specified.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; } = HttpStatusCode.InternalServerError;

        /// <summary>
        /// Gets a read-only dictionary of validation errors related to the failure.
        /// Defaults to an empty dictionary if no errors are provided.
        /// </summary>
        public ReadOnlyDictionary<string, string[]> Errors { get; } = ReadOnlyDictionary<string, string[]>.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpFailure"/> class with default values.
        /// The HTTP status code defaults to <see cref="HttpStatusCode.InternalServerError"/>
        /// and the errors dictionary is empty.
        /// </summary>
        public HttpFailure()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpFailure"/> class with a specified HTTP status code.
        /// The errors dictionary defaults to empty.
        /// </summary>
        /// <param name="httpStatusCode">The HTTP status code associated with the failure.</param>
        public HttpFailure(HttpStatusCode httpStatusCode)
        {
            HttpStatusCode = httpStatusCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpFailure"/> class with a specified errors dictionary.
        /// The HTTP status code defaults to <see cref="HttpStatusCode.InternalServerError"/>.
        /// </summary>
        /// <param name="errors">A dictionary of validation errors related to the failure.</param>
        public HttpFailure(IDictionary<string, string[]> errors)
        {
            // Converts the provided errors dictionary to a read-only dictionary
            Errors = new ReadOnlyDictionary<string, string[]>(errors);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpFailure"/> class with a specified HTTP status code and errors dictionary.
        /// </summary>
        /// <param name="httpStatusCode">The HTTP status code associated with the failure.</param>
        /// <param name="errors">A dictionary of validation errors related to the failure.</param>
        public HttpFailure(HttpStatusCode httpStatusCode, IDictionary<string, string[]> errors)
        {
            HttpStatusCode = httpStatusCode;
            Errors = new ReadOnlyDictionary<string, string[]>(errors);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpFailure"/> class with a specified HTTP status code, message, and errors dictionary.
        /// </summary>
        /// <param name="httpStatusCode">The HTTP status code associated with the failure.</param>
        /// <param name="message">The message that describes the failure.</param>
        /// <param name="errors">A dictionary of validation errors related to the failure.</param>
        /// <exception cref="ArgumentException">Thrown if the message is null or empty.</exception>
        public HttpFailure(HttpStatusCode httpStatusCode, string message, IDictionary<string, string[]> errors)
            : base(message)
        {
            HttpStatusCode = httpStatusCode;
            Errors = new ReadOnlyDictionary<string, string[]>(errors);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpFailure"/> class with a specified message and errors dictionary.
        /// The HTTP status code defaults to <see cref="HttpStatusCode.InternalServerError"/>.
        /// </summary>
        /// <param name="message">The message that describes the failure.</param>
        /// <param name="errors">A dictionary of validation errors related to the failure.</param>
        /// <exception cref="ArgumentException">Thrown if the message is null or empty.</exception>
        public HttpFailure(string message, IDictionary<string, string[]> errors)
            : base(message)
        {
            Errors = new ReadOnlyDictionary<string, string[]>(errors);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpFailure"/> class with a specified HTTP status code, exception, and errors dictionary.
        /// </summary>
        /// <param name="httpStatusCode">The HTTP status code associated with the failure.</param>
        /// <param name="exception">The exception that provides more details about the failure.</param>
        /// <param name="errors">A dictionary of validation errors related to the failure.</param>
        /// <exception cref="ArgumentNullException">Thrown if the exception is null.</exception>
        public HttpFailure(HttpStatusCode httpStatusCode, Exception exception, IDictionary<string, string[]> errors)
            : base(exception)
        {
            HttpStatusCode = httpStatusCode;
            Errors = new ReadOnlyDictionary<string, string[]>(errors);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpFailure"/> class with a specified exception and errors dictionary.
        /// The HTTP status code defaults to <see cref="HttpStatusCode.InternalServerError"/>.
        /// </summary>
        /// <param name="exception">The exception that provides more details about the failure.</param>
        /// <param name="errors">A dictionary of validation errors related to the failure.</param>
        /// <exception cref="ArgumentNullException">Thrown if the exception is null.</exception>
        public HttpFailure(Exception exception, IDictionary<string, string[]> errors)
            : base(exception)
        {
            // Converts the provided errors dictionary to a read-only dictionary
            Errors = new ReadOnlyDictionary<string, string[]>(errors);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpFailure"/> class with a specified HTTP status code, message, exception, and errors dictionary.
        /// </summary>
        /// <param name="httpStatusCode">The HTTP status code associated with the failure.</param>
        /// <param name="message">The message that describes the failure.</param>
        /// <param name="exception">The exception that provides more details about the failure.</param>
        /// <param name="errors">A dictionary of validation errors related to the failure.</param>
        /// <exception cref="ArgumentException">Thrown if the message is null or empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the exception is null.</exception>
        public HttpFailure(
            HttpStatusCode httpStatusCode,
            string message,
            Exception exception,
            IDictionary<string, string[]> errors)
            : base(message, exception)
        {
            HttpStatusCode = httpStatusCode;
            Errors = new ReadOnlyDictionary<string, string[]>(errors);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpFailure"/> class with a specified message, exception, and errors dictionary.
        /// The HTTP status code defaults to <see cref="HttpStatusCode.InternalServerError"/>.
        /// </summary>
        /// <param name="message">The message that describes the failure.</param>
        /// <param name="exception">The exception that provides more details about the failure.</param>
        /// <param name="errors">A dictionary of validation errors related to the failure.</param>
        /// <exception cref="ArgumentException">Thrown if the message is null or empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the exception is null.</exception>
        public HttpFailure(string message, Exception exception, IDictionary<string, string[]> errors)
            : base(message, exception)
        {
            Errors = new ReadOnlyDictionary<string, string[]>(errors);
        }
    }
}