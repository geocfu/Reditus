using System;
using Reditus.Extensions.AspNetCore.Http.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http;

/// <summary>
/// Represents a HttpResult object.
/// </summary>
public class HttpResult : Result, IHttpResult
{
    /// <inheritdoc />
    public new IHttpSuccess Success => (IHttpSuccess)base.Success;

    /// <inheritdoc />
    public new IHttpFailure Failure => (IHttpFailure)base.Failure;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="success"></param>
    protected HttpResult(IHttpSuccess success)
        : base(success)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="failure"></param>
    protected HttpResult(IHttpFailure failure)
        : base(failure)
    {
    }

    /// <summary>
    /// Creates a successful HttpResult.
    /// </summary>
    /// <param name="httpSuccess">The success object of the HttpResult.</param>
    /// <returns>A successful Result instance.</returns>
    public static HttpResult CreateSuccess(IHttpSuccess httpSuccess)
    {
        return new HttpResult(httpSuccess);
    }

    /// <summary>
    /// Creates a successful Result from a different successful HttpResult.
    /// </summary>
    /// <param name="httpResult">The HttpResult to copy from.</param>
    /// <returns>A failed HttpResult instance.</returns>
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
    /// Creates a failed Result.
    /// </summary>
    /// <param name="failure">The failure object of the HttpResult.</param>
    /// <returns>A failed Result instance.</returns>
    public static HttpResult CreateFail(IHttpFailure failure)
    {
        return new HttpResult(failure);
    }

    /// <summary>
    /// Creates a failed Result from a different failed HttpResult.
    /// </summary>
    /// <param name="httpResult">The Result to copy from.</param>
    /// <returns>A failed HttpResult instance.</returns>
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