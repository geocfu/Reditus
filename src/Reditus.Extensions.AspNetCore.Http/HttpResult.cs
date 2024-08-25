using System;
using Reditus.Extensions.AspNetCore.Http.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http;

/// <summary>
/// Represents an HTTP result that can either be a success or a failure.
/// This class extends <see cref="Result"/> and implements <see cref="IHttpResult"/>.
/// It provides methods to create and handle HTTP responses based on the success or failure state.
/// </summary>
public class HttpResult : Result, IHttpResult
{
    /// <summary>
    /// Gets the successful outcome of the HTTP result, cast to <see cref="IHttpSuccess"/>.
    /// </summary>
    public new IHttpSuccess Success => (IHttpSuccess)base.Success;

    /// <summary>
    /// Gets the failed outcome of the HTTP result, cast to <see cref="IHttpFailure"/>.
    /// </summary>
    public new IHttpFailure Failure => (IHttpFailure)base.Failure;

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpResult"/> class with a successful outcome.
    /// </summary>
    /// <param name="success">The success object representing the successful HTTP result.</param>
    public HttpResult(IHttpSuccess success)
        : base(success)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpResult"/> class with a failed outcome.
    /// </summary>
    /// <param name="failure">The failure object representing the failed HTTP result.</param>
    public HttpResult(IHttpFailure failure)
        : base(failure)
    {
    }

    /// <summary>
    /// Creates a new successful <see cref="HttpResult"/> instance from an <see cref="IHttpSuccess"/> object.
    /// </summary>
    /// <param name="httpSuccess">The success object to use for creating the <see cref="HttpResult"/>.</param>
    /// <returns>A new <see cref="HttpResult"/> instance representing a successful outcome.</returns>
    public static HttpResult CreateSuccess(IHttpSuccess httpSuccess)
    {
        return new HttpResult(httpSuccess);
    }

    /// <summary>
    /// Creates a new successful <see cref="HttpResult"/> instance by copying from an existing <see cref="HttpResult"/>.
    /// </summary>
    /// <param name="httpResult">The <see cref="HttpResult"/> instance to copy from.</param>
    /// <returns>A new <see cref="HttpResult"/> instance representing a successful outcome.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="httpResult"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the provided <paramref name="httpResult"/> is a failure.</exception>
    public static HttpResult CreateSuccess(HttpResult httpResult)
    {
        ArgumentNullException.ThrowIfNull(httpResult);

        if (httpResult.IsFailed)
        {
            throw new InvalidOperationException("Converting a Failed Result to a Successful Result is invalid.");
        }

        return CreateSuccess(httpResult.Success);
    }

    /// <summary>
    /// Creates a new failed <see cref="HttpResult"/> instance from an <see cref="IHttpFailure"/> object.
    /// </summary>
    /// <param name="failure">The failure object to use for creating the <see cref="HttpResult"/>.</param>
    /// <returns>A new <see cref="HttpResult"/> instance representing a failed outcome.</returns>
    public static HttpResult CreateFail(IHttpFailure failure)
    {
        return new HttpResult(failure);
    }

    /// <summary>
    /// Creates a new failed <see cref="HttpResult"/> instance by copying from an existing <see cref="HttpResult"/>.
    /// </summary>
    /// <param name="httpResult">The <see cref="HttpResult"/> instance to copy from.</param>
    /// <returns>A new <see cref="HttpResult"/> instance representing a failed outcome.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="httpResult"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the provided <paramref name="httpResult"/> is a success.</exception>
    public static HttpResult CreateFail(HttpResult httpResult)
    {
        ArgumentNullException.ThrowIfNull(httpResult);

        if (httpResult.IsSuccessful)
        {
            throw new InvalidOperationException("Converting a Successful Result to a Failed Result is invalid.");
        }

        return CreateFail(httpResult.Failure);
    }
}