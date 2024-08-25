using System;
using Reditus.Extensions.AspNetCore.Http.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http;

/// <summary>
/// Represents an HTTP result that includes a value of type <typeparamref name="T"/>.
/// This class extends <see cref="Result{T}"/> and implements <see cref="IHttpResult{T}"/>.
/// It provides a structure for handling HTTP responses that include a value or indicate failure.
/// </summary>
/// <typeparam name="T">The type of the value contained in the <see cref="HttpResult{T}"/>.</typeparam>
public class HttpResult<T> : Result<T>, IHttpResult<T>
{
    /// <inheritdoc />
    public new IHttpSuccess<T> Success => (IHttpSuccess<T>)base.Success;

    /// <inheritdoc />
    public new IHttpFailure Failure => (IHttpFailure)base.Failure;

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpResult{T}"/> class with a successful outcome.
    /// </summary>
    /// <param name="success">The success object of the result, containing the value of type <typeparamref name="T"/>.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="success"/> is null.</exception>
    public HttpResult(IHttpSuccess<T> success)
        : base(success)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpResult{T}"/> class with a failed outcome.
    /// </summary>
    /// <param name="failure">The failure object of the result, indicating the error details.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="failure"/> is null.</exception>
    public HttpResult(IHttpFailure failure)
        : base(failure)
    {
    }

    /// <summary>
    /// Creates a successful <see cref="HttpResult{T}"/> instance from an <see cref="IHttpSuccess{T}"/> object.
    /// </summary>
    /// <param name="httpSuccess">The success object to use for creating the <see cref="HttpResult{T}"/>.</param>
    /// <returns>A new <see cref="HttpResult{T}"/> instance representing a successful outcome.</returns>
    public static HttpResult<T> CreateSuccess(IHttpSuccess<T> httpSuccess)
    {
        return new HttpResult<T>(httpSuccess);
    }

    /// <summary>
    /// Creates a successful <see cref="HttpResult{T}"/> instance by copying from an existing successful <see cref="HttpResult{T}"/>.
    /// </summary>
    /// <param name="httpResult">The <see cref="HttpResult{T}"/> instance to copy from.</param>
    /// <returns>A new <see cref="HttpResult{T}"/> instance representing a successful outcome.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="httpResult"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the provided <paramref name="httpResult"/> represents a failed outcome.</exception>
    public static HttpResult<T> CreateSuccess(HttpResult<T> httpResult)
    {
        ArgumentNullException.ThrowIfNull(httpResult);

        if (httpResult.IsFailed)
        {
            throw new InvalidOperationException("Converting a Failed Result to a Successful Result is invalid.");
        }

        return CreateSuccess(httpResult.Success);
    }

    /// <summary>
    /// Creates a failed <see cref="HttpResult{T}"/> instance from an <see cref="IHttpFailure"/> object.
    /// </summary>
    /// <param name="failure">The failure object to use for creating the <see cref="HttpResult{T}"/>.</param>
    /// <returns>A new <see cref="HttpResult{T}"/> instance representing a failed outcome.</returns>
    public static HttpResult<T> CreateFail(IHttpFailure failure)
    {
        return new HttpResult<T>(failure);
    }

    /// <summary>
    /// Creates a failed <see cref="HttpResult{T}"/> instance by copying from an existing failed <see cref="HttpResult{T}"/>.
    /// </summary>
    /// <param name="httpResult">The <see cref="HttpResult{T}"/> instance to copy from.</param>
    /// /// <typeparam name="TY">The type of the <see cref="HttpResult{T}"/> instance to copy from.</typeparam>
    /// <returns>A new <see cref="HttpResult{T}"/> instance representing a failed outcome.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="httpResult"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the provided <paramref name="httpResult"/> represents a successful outcome.</exception>
    public static HttpResult<T> CreateFail<TY>(HttpResult<TY> httpResult)
    {
        ArgumentNullException.ThrowIfNull(httpResult);

        if (httpResult.IsSuccessful)
        {
            throw new InvalidOperationException("Converting a Successful Result to a Failed Result is invalid.");
        }

        return CreateFail(httpResult.Failure);
    }
}