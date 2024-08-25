using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reditus.Extensions.AspNetCore.Http.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Converters;

/// <summary>
/// Provides extension methods to convert <see cref="HttpResult"/> and <see cref="HttpResult{T}"/> instances
/// into <see cref="ActionResult"/> and <see cref="ActionResult{T}"/> instances, respectively.
/// </summary>
public static class HttpResultToActionResultConverter
{
    /// <summary>
    /// Converts an <see cref="HttpResult{T}"/> to an <see cref="ActionResult{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the result value.</typeparam>
    /// <param name="httpResult">The HTTP result to convert.</param>
    /// <returns>An <see cref="ActionResult{T}"/> representing the HTTP result.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="httpResult"/> is null.</exception>
    public static ActionResult<T> ToActionResult<T>(this HttpResult<T> httpResult)
    {
        ArgumentNullException.ThrowIfNull(httpResult);

        if (httpResult.IsSuccessful)
        {
            return CreateObjectResult(httpResult.Success);
        }

        return CreateProblemObjectResult(httpResult.Failure);
    }

    /// <summary>
    /// Asynchronously converts a <see cref="Task"/> of an <see cref="HttpResult{T}"/> to an <see cref="ActionResult{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the result value.</typeparam>
    /// <param name="httpResultTask">The task representing the HTTP result to convert.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is an <see cref="ActionResult{T}"/> representing the HTTP result.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="httpResultTask"/> is null.</exception>
    public static async Task<ActionResult<T>> ToActionResult<T>(this Task<HttpResult<T>> httpResultTask)
    {
        ArgumentNullException.ThrowIfNull(httpResultTask);

        var httpResult = await httpResultTask;

        if (httpResult.IsSuccessful)
        {
            return CreateObjectResult(httpResult.Success);
        }

        return CreateProblemObjectResult(httpResult.Failure);
    }

    /// <summary>
    /// Converts an <see cref="HttpResult"/> to an <see cref="ActionResult"/>.
    /// </summary>
    /// <param name="httpResult">The HTTP result to convert.</param>
    /// <returns>An <see cref="ActionResult"/> representing the HTTP result.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="httpResult"/> is null.</exception>
    public static ActionResult ToActionResult(this HttpResult httpResult)
    {
        ArgumentNullException.ThrowIfNull(httpResult);

        if (httpResult.IsSuccessful)
        {
            return CreateObjectResult(httpResult.Success);
        }

        return CreateProblemObjectResult(httpResult.Failure);
    }

    /// <summary>
    /// Asynchronously converts a <see cref="Task"/> of an <see cref="HttpResult"/> to an <see cref="ActionResult"/>.
    /// </summary>
    /// <param name="httpResultTask">The task representing the HTTP result to convert.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is an <see cref="ActionResult"/> representing the HTTP result.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="httpResultTask"/> is null.</exception>
    public static async Task<ActionResult> ToActionResult(this Task<HttpResult> httpResultTask)
    {
        ArgumentNullException.ThrowIfNull(httpResultTask);

        var httpResult = await httpResultTask;

        if (httpResult.IsSuccessful)
        {
            return CreateObjectResult(httpResult.Success);
        }

        return CreateProblemObjectResult(httpResult.Failure);
    }

    /// <summary>
    /// Creates an <see cref="ObjectResult"/> for a successful HTTP result with a value of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the result value.</typeparam>
    /// <param name="httpSuccess">The successful HTTP result.</param>
    /// <returns>An <see cref="ObjectResult"/> representing the successful result.</returns>
    private static ObjectResult CreateObjectResult<T>(IHttpSuccess<T> httpSuccess)
    {
        return new ObjectResult(httpSuccess.Value)
        {
            StatusCode = (int)httpSuccess.StatusCode,
        };
    }

    /// <summary>
    /// Creates an <see cref="ObjectResult"/> for a successful HTTP result without a value.
    /// </summary>
    /// <param name="httpSuccess">The successful HTTP result.</param>
    /// <returns>An <see cref="ObjectResult"/> representing the successful result.</returns>
    private static ObjectResult CreateObjectResult(IHttpSuccess httpSuccess)
    {
        return new ObjectResult(null)
        {
            StatusCode = (int)httpSuccess.StatusCode,
        };
    }

    /// <summary>
    /// Creates an <see cref="ObjectResult"/> for a failed HTTP result.
    /// </summary>
    /// <param name="httpFailure">The failed HTTP result.</param>
    /// <returns>An <see cref="ObjectResult"/> representing the failure.</returns>
    private static ObjectResult CreateProblemObjectResult(IHttpFailure httpFailure)
    {
        if (httpFailure is IHttpBadRequest httpBadRequestFailure)
        {
            var validationProblemDetails = new ValidationProblemDetails
            {
                Type = httpBadRequestFailure.Type,
                Title = httpBadRequestFailure.Title,
                Detail = httpBadRequestFailure.Message,
                Errors = httpBadRequestFailure.ValidationErrors,
            };

            return new ObjectResult(validationProblemDetails)
            {
                StatusCode = (int)httpFailure.StatusCode,
            };
        }

        var problemDetails = new ProblemDetails
        {
            Type = httpFailure.Type,
            Title = httpFailure.Title,
            Detail = httpFailure.Message,
        };

        return new ObjectResult(problemDetails)
        {
            StatusCode = (int)httpFailure.StatusCode,
        };
    }
}