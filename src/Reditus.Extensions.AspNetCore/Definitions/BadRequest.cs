using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using Reditus.Definitions;
using Reditus.Extensions.AspNetCore.Abstractions;

namespace Reditus.Extensions.AspNetCore.Definitions
{
    /// <summary>
    ///
    /// </summary>
    public class BadRequest : Failure, IHttpFailure
    {
        /// <summary>
        ///
        /// </summary>
        public HttpStatusCode Status => HttpStatusCode.BadRequest;

        /// <summary>
        ///
        /// </summary>
        public ReadOnlyDictionary<string, string[]> ValidationErrors { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="validationErrors"></param>
        public BadRequest(IDictionary<string, string[]> validationErrors)
        {
            ValidationErrors = validationErrors.AsReadOnly();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <param name="validationErrors"></param>
        public BadRequest(string message, IDictionary<string, string[]> validationErrors)
            : base(message)
        {
            ValidationErrors = validationErrors.AsReadOnly();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="validationErrors"></param>
        public BadRequest(Exception exception, IDictionary<string, string[]> validationErrors)
            : base(exception)
        {
            ValidationErrors = validationErrors.AsReadOnly();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="validationErrors"></param>
        public BadRequest(string message, Exception exception, IDictionary<string, string[]> validationErrors)
            : base(message, exception)
        {
            ValidationErrors = validationErrors.AsReadOnly();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <param name="validationErrors"></param>
        public BadRequest(IEnumerable<KeyValuePair<string, string>> validationErrors)
        {
            ValidationErrors = validationErrors
                .GroupBy(x => x.Key)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.Value).ToArray()
                ).AsReadOnly();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <param name="validationErrors"></param>
        public BadRequest(string message, IEnumerable<KeyValuePair<string, string>> validationErrors)
            : base(message)
        {
            ValidationErrors = validationErrors
                .GroupBy(x => x.Key)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.Value).ToArray()
                ).AsReadOnly();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="validationErrors"></param>
        public BadRequest(Exception exception, IEnumerable<KeyValuePair<string, string>> validationErrors)
            : base(exception)
        {
            ValidationErrors = validationErrors
                .GroupBy(x => x.Key)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.Value).ToArray()
                ).AsReadOnly();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="validationErrors"></param>
        public BadRequest(string message, Exception exception,
            IEnumerable<KeyValuePair<string, string>> validationErrors)
            : base(message, exception)
        {
            ValidationErrors = validationErrors
                .GroupBy(x => x.Key)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.Value).ToArray()
                ).AsReadOnly();
        }
    }
}