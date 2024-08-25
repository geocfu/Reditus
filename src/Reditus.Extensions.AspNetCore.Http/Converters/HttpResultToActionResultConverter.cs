using System;
using Microsoft.AspNetCore.Mvc;

namespace Reditus.Extensions.AspNetCore.Http.Converters;

public static class ConvertHttpResultToActionResult
{
    public static ActionResult<T> ToActionResult<T>(this HttpResult<T> httpResult)
    {
        ArgumentNullException.ThrowIfNull(httpResult);

        if (httpResult.IsSuccessful)
        {
            return new ObjectResult(httpResult.Success.Value)
            {
                StatusCode = (int)httpResult.Success.StatusCode
            };
        }

        var problemDetails = new ProblemDetails
        {
            Type = httpResult.Failure.Type,
            Title = httpResult.Failure.Title,
            Detail = httpResult.Failure.Message,
        };

        return new ObjectResult(problemDetails)
        {
            StatusCode = (int)httpResult.Failure.StatusCode
        };
    }

    public static ActionResult ToActionResult(this HttpResult httpResult)
    {
        ArgumentNullException.ThrowIfNull(httpResult);

        if (httpResult.IsSuccessful)
        {
            return new ObjectResult(null)
            {
                StatusCode = (int)httpResult.Success.StatusCode
            };
        }

        var problemDetails = new ProblemDetails
        {
            Type = httpResult.Failure.Type,
            Title = httpResult.Failure.Title,
            Detail = httpResult.Failure.Message,
        };

        return new ObjectResult(problemDetails)
        {
            StatusCode = (int)httpResult.Failure.StatusCode
        };
    }
}