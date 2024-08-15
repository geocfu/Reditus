using System;
using Reditus.Extensions.AspNetCore.Http.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http;

/// <summary>
/// Represents a HttpResult{T} object.
/// </summary>
/// <typeparam name="T">The type contained in <see cref="HttpResult{T}" />.</typeparam>
public class HttpResult<T> : Result<T>, IHttpResult<T>
{
    /// <inheritdoc />
    public new IHttpSuccess<T> Success => (IHttpSuccess<T>)base.Success;

    /// <inheritdoc />
    public new IHttpFailure Failure => (IHttpFailure)base.Failure;

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpResult{T}"/> class.
    /// </summary>
    /// <param name="success">The success of the Result.</param>
    /// <exception cref="ArgumentNullException">Thrown when success is null.</exception>
    protected HttpResult(IHttpSuccess<T> success)
        : base(success)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpResult{T}"/> class.
    /// </summary>
    /// <param name="failure">The failure of the HttpResult.</param>
    /// <exception cref="ArgumentNullException">Thrown when failure is null.</exception>
    protected HttpResult(IHttpFailure failure)
        : base(failure)
    {
    }

    /// <summary>
    /// Creates a successful HttpResult.
    /// </summary>
    /// <param name="httpSuccess">The success object of the HttpResult.</param>
    /// <returns>A successful Result instance.</returns>
    public static HttpResult<T> CreateSuccess(IHttpSuccess<T> httpSuccess)
    {
        return new HttpResult<T>(httpSuccess);
    }

    /// <summary>
    /// Creates a successful Result from a different successful HttpResult.
    /// </summary>
    /// <param name="httpResult">The HttpResult to copy from.</param>
    /// <returns>A failed HttpResult instance.</returns>
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
    /// Creates a failed Result.
    /// </summary>
    /// <param name="failure">The failure object of the HttpResult.</param>
    /// <returns>A failed Result instance.</returns>
    public static HttpResult<T> CreateFail(IHttpFailure failure)
    {
        return new HttpResult<T>(failure);
    }

    /// <summary>
    /// Creates a failed Result from a different failed HttpResult.
    /// </summary>
    /// <param name="httpResult">The Result to copy from.</param>
    /// <returns>A failed HttpResult instance.</returns>
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