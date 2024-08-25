using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using Reditus.Definitions;
using Reditus.Extensions.AspNetCore.Http.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Definitions;

/// <summary>
/// Represents a concrete implementation of a "Bad Request" failure (HTTP 400).
/// This class extends the <see cref="Failure"/> base class and implements the <see cref="IHttpBadRequest"/> interface.
/// It handles validation errors and provides details for HTTP 400 responses.
/// </summary>
public class HttpBadRequest : Failure, IHttpBadRequest
{
    /// <inheritdoc />
    public string Type => "https://tools.ietf.org/html/rfc9110#section-15.5.1";

    /// <inheritdoc />
    public string Title => "Bad Request";

    /// <inheritdoc />
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    /// <summary>
    /// Gets a read-only dictionary that holds validation errors.
    /// The keys represent the names of the invalid fields or parameters,
    /// and the values are arrays of corresponding error messages.
    /// </summary>
    public ReadOnlyDictionary<string, string[]> ValidationErrors { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpBadRequest"/> class with the specified validation errors.
    /// </summary>
    /// <param name="validationErrors">A dictionary containing the validation errors.</param>
    public HttpBadRequest(IDictionary<string, string[]> validationErrors)
    {
        ValidationErrors = validationErrors.AsReadOnly();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpBadRequest"/> class with a message and validation errors.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="validationErrors">A dictionary containing the validation errors.</param>
    public HttpBadRequest(string message, IDictionary<string, string[]> validationErrors)
        : base(message)
    {
        ValidationErrors = validationErrors.AsReadOnly();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpBadRequest"/> class with an exception and validation errors.
    /// </summary>
    /// <param name="exception">The exception that caused the failure.</param>
    /// <param name="validationErrors">A dictionary containing the validation errors.</param>
    public HttpBadRequest(Exception exception, IDictionary<string, string[]> validationErrors)
        : base(exception)
    {
        ValidationErrors = validationErrors.AsReadOnly();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpBadRequest"/> class with a message, exception, and validation errors.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="exception">The exception that caused the failure.</param>
    /// <param name="validationErrors">A dictionary containing the validation errors.</param>
    public HttpBadRequest(string message, Exception exception, IDictionary<string, string[]> validationErrors)
        : base(message, exception)
    {
        ValidationErrors = validationErrors.AsReadOnly();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpBadRequest"/> class using a collection of key-value pairs.
    /// The collection is grouped by key to build the validation errors dictionary.
    /// </summary>
    /// <param name="validationErrors">A collection of key-value pairs representing field names and their errors.</param>
    public HttpBadRequest(IEnumerable<KeyValuePair<string, string>> validationErrors)
    {
        ValidationErrors = validationErrors
            .GroupBy(x => x.Key)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.Value).ToArray())
            .AsReadOnly();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpBadRequest"/> class with a message and a collection of key-value pairs.
    /// The collection is grouped by key to build the validation errors dictionary.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="validationErrors">A collection of key-value pairs representing field names and their errors.</param>
    public HttpBadRequest(string message, IEnumerable<KeyValuePair<string, string>> validationErrors)
        : base(message)
    {
        ValidationErrors = validationErrors
            .GroupBy(x => x.Key)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.Value).ToArray())
            .AsReadOnly();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpBadRequest"/> class with an exception and a collection of key-value pairs.
    /// The collection is grouped by key to build the validation errors dictionary.
    /// </summary>
    /// <param name="exception">The exception that caused the failure.</param>
    /// <param name="validationErrors">A collection of key-value pairs representing field names and their errors.</param>
    public HttpBadRequest(Exception exception, IEnumerable<KeyValuePair<string, string>> validationErrors)
        : base(exception)
    {
        ValidationErrors = validationErrors
            .GroupBy(x => x.Key)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.Value).ToArray())
            .AsReadOnly();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpBadRequest"/> class with a message, exception, and a collection of key-value pairs.
    /// The collection is grouped by key to build the validation errors dictionary.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="exception">The exception that caused the failure.</param>
    /// <param name="validationErrors">A collection of key-value pairs representing field names and their errors.</param>
    public HttpBadRequest(
        string message,
        Exception exception,
        IEnumerable<KeyValuePair<string, string>> validationErrors)
        : base(message, exception)
    {
        ValidationErrors = validationErrors
            .GroupBy(x => x.Key)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.Value).ToArray())
            .AsReadOnly();
    }

    /// <inheritdoc />
    public static HttpResult CreateHttpResult(IDictionary<string, string[]> validationErrors)
    {
        var badRequest = new HttpBadRequest(validationErrors);
        return HttpResult.CreateFail(badRequest);
    }

    /// <inheritdoc />
    public static HttpResult CreateHttpResult(string message, IDictionary<string, string[]> validationErrors)
    {
        var badRequest = new HttpBadRequest(message, validationErrors);
        return HttpResult.CreateFail(badRequest);
    }

    /// <inheritdoc />
    public static HttpResult CreateHttpResult(Exception exception, IDictionary<string, string[]> validationErrors)
    {
        var badRequest = new HttpBadRequest(exception, validationErrors);
        return HttpResult.CreateFail(badRequest);
    }

    /// <inheritdoc />
    public static HttpResult CreateHttpResult(
        string message,
        Exception exception,
        IDictionary<string, string[]> validationErrors)
    {
        var badRequest = new HttpBadRequest(message, exception, validationErrors);
        return HttpResult.CreateFail(badRequest);
    }

    /// <inheritdoc />
    public static HttpResult CreateHttpResult(IEnumerable<KeyValuePair<string, string>> validationErrors)
    {
        var badRequest = new HttpBadRequest(validationErrors);
        return HttpResult.CreateFail(badRequest);
    }

    /// <inheritdoc />
    public static HttpResult CreateHttpResult(
        string message,
        IEnumerable<KeyValuePair<string, string>> validationErrors)
    {
        var badRequest = new HttpBadRequest(message, validationErrors);
        return HttpResult.CreateFail(badRequest);
    }

    /// <inheritdoc />
    public static HttpResult CreateHttpResult(
        Exception exception,
        IEnumerable<KeyValuePair<string, string>> validationErrors)
    {
        var badRequest = new HttpBadRequest(exception, validationErrors);
        return HttpResult.CreateFail(badRequest);
    }

    /// <inheritdoc />
    public static HttpResult CreateHttpResult(
        string message,
        Exception exception,
        IEnumerable<KeyValuePair<string, string>> validationErrors)
    {
        var badRequest = new HttpBadRequest(message, exception, validationErrors);
        return HttpResult.CreateFail(badRequest);
    }
}