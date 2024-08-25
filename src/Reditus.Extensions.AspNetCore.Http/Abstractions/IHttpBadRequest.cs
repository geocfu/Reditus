using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Reditus.Extensions.AspNetCore.Http.Abstractions;

/// <summary>
/// Represents an interface for handling HTTP 400 Bad Request failures.
/// Provides a structure for validation errors and other failure details.
/// </summary>
public interface IBadRequest : IHttpFailure
{
    /// <summary>
    /// Gets a read-only dictionary that contains validation errors.
    /// The key represents the name of the field or parameter, 
    /// and the value is an array of error messages associated with that field.
    /// </summary>
    public ReadOnlyDictionary<string, string[]> ValidationErrors { get; }

    /// <summary>
    /// Creates a failed <see cref="HttpResult"/> with validation errors.
    /// This method allows the creation of a failed result with validation errors
    /// from a dictionary of field names and associated error messages.
    /// </summary>
    /// <param name="validationErrors">A dictionary of validation errors where the key is a field name and the value is an array of error messages.</param>
    /// <returns>A failed <see cref="HttpResult"/> instance containing the provided validation errors.</returns>
    public static abstract HttpResult CreateHttpResult(IDictionary<string, string[]> validationErrors);

    /// <summary>
    /// Creates a failed <see cref="HttpResult"/> with a message and validation errors.
    /// This method allows the creation of a failed result with a custom error message
    /// and validation errors.
    /// </summary>
    /// <param name="message">The error message to be included in the result.</param>
    /// <param name="validationErrors">A dictionary of validation errors where the key is a field name and the value is an array of error messages.</param>
    /// <returns>A failed <see cref="HttpResult"/> instance containing the provided error message and validation errors.</returns>
    public static abstract HttpResult CreateHttpResult(string message,
        IDictionary<string, string[]> validationErrors);

    /// <summary>
    /// Creates a failed <see cref="HttpResult"/> with an exception and validation errors.
    /// This method allows the creation of a failed result with an exception and validation errors.
    /// </summary>
    /// <param name="exception">The exception that caused the failure.</param>
    /// <param name="validationErrors">A dictionary of validation errors where the key is a field name and the value is an array of error messages.</param>
    /// <returns>A failed <see cref="HttpResult"/> instance containing the provided exception and validation errors.</returns>
    public static abstract HttpResult CreateHttpResult(Exception exception,
        IDictionary<string, string[]> validationErrors);

    /// <summary>
    /// Creates a failed <see cref="HttpResult"/> with a message, an exception, and validation errors.
    /// This method allows the creation of a failed result with a custom error message, an exception,
    /// and validation errors.
    /// </summary>
    /// <param name="message">The error message to be included in the result.</param>
    /// <param name="exception">The exception that caused the failure.</param>
    /// <param name="validationErrors">A dictionary of validation errors where the key is a field name and the value is an array of error messages.</param>
    /// <returns>A failed <see cref="HttpResult"/> instance containing the provided message, exception, and validation errors.</returns>
    public static abstract HttpResult CreateHttpResult(string message, Exception exception,
        IDictionary<string, string[]> validationErrors);

    /// <summary>
    /// Creates a failed <see cref="HttpResult"/> with validation errors represented as key-value pairs.
    /// This method allows the creation of a failed result with validation errors
    /// from an enumerable of key-value pairs where each pair represents a field name and an associated error message.
    /// </summary>
    /// <param name="validationErrors">An enumerable of key-value pairs where each key is a field name and each value is an error message.</param>
    /// <returns>A failed <see cref="HttpResult"/> instance containing the provided validation errors.</returns>
    public static abstract HttpResult
        CreateHttpResult(IEnumerable<KeyValuePair<string, string>> validationErrors);

    /// <summary>
    /// Creates a failed <see cref="HttpResult"/> with a message and validation errors represented as key-value pairs.
    /// This method allows the creation of a failed result with a custom error message
    /// and validation errors from an enumerable of key-value pairs.
    /// </summary>
    /// <param name="message">The error message to be included in the result.</param>
    /// <param name="validationErrors">An enumerable of key-value pairs where each key is a field name and each value is an error message.</param>
    /// <returns>A failed <see cref="HttpResult"/> instance containing the provided message and validation errors.</returns>
    public static abstract HttpResult CreateHttpResult(string message,
        IEnumerable<KeyValuePair<string, string>> validationErrors);

    /// <summary>
    /// Creates a failed <see cref="HttpResult"/> with an exception and validation errors represented as key-value pairs.
    /// This method allows the creation of a failed result with an exception and validation errors
    /// from an enumerable of key-value pairs.
    /// </summary>
    /// <param name="exception">The exception that caused the failure.</param>
    /// <param name="validationErrors">An enumerable of key-value pairs where each key is a field name and each value is an error message.</param>
    /// <returns>A failed <see cref="HttpResult"/> instance containing the provided exception and validation errors.</returns>
    public static abstract HttpResult CreateHttpResult(Exception exception,
        IEnumerable<KeyValuePair<string, string>> validationErrors);

    /// <summary>
    /// Creates a failed <see cref="HttpResult"/> with a message, an exception, and validation errors represented as key-value pairs.
    /// This method allows the creation of a failed result with a custom error message, an exception,
    /// and validation errors from an enumerable of key-value pairs.
    /// </summary>
    /// <param name="message">The error message to be included in the result.</param>
    /// <param name="exception">The exception that caused the failure.</param>
    /// <param name="validationErrors">An enumerable of key-value pairs where each key is a field name and each value is an error message.</param>
    /// <returns>A failed <see cref="HttpResult"/> instance containing the provided message, exception, and validation errors.</returns>
    public static abstract HttpResult CreateHttpResult(string message, Exception exception,
        IEnumerable<KeyValuePair<string, string>> validationErrors);
}