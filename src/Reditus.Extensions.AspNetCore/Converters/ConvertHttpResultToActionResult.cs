using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Reditus.Extensions.AspNetCore.Abstractions;

namespace Reditus.Extensions.AspNetCore.Core;

public static class ConvertHttpResultToActionResult
{
    public static ActionResult<T> ToActionResult<T>(this Result result)
    {
        ArgumentNullException.ThrowIfNull(result);

        if (result.IsSuccessful)
        {
            if (result.Success is IHttpSuccess<T> aspNetCoreSuccess)
            {
                return new ObjectResult(aspNetCoreSuccess.Value)
                {
                    StatusCode = (int)aspNetCoreSuccess.HttpStatusCode
                };
            }

            throw new Exception();
        }

        if (result.Failure is IHttpFailure aspNetCoreFailure)
        {
            var problemDetails = new ProblemDetails
            {
                Title = aspNetCoreFailure.Message
            };
            return new ObjectResult(problemDetails)
            {
                StatusCode = (int)aspNetCoreFailure.Status
            };
        }

        throw new Exception();
    }

    public static ActionResult ToActionResult(this Result result)
    {
        ArgumentNullException.ThrowIfNull(result);

        if (result.IsSuccessful)
        {
            if (result.Success is IHttpSuccess aspNetCoreSuccess)
            {
                return new ObjectResult(null)
                {
                    StatusCode = (int)aspNetCoreSuccess.HttpStatusCode
                };
            }

            // the result .Success object did not inherit from IHttpSuccess
            // throw exception
            throw new Exception();
        }

        if (result.Failure is IHttpFailure aspNetCoreFailure)
        {
            var problemDetails = new ProblemDetails
            {
                Title = aspNetCoreFailure.Message,
                Detail = aspNetCoreFailure.Message,
            };
            return new ObjectResult(problemDetails)
            {
                StatusCode = (int)aspNetCoreFailure.Status
            };
        }

        throw new Exception();
    }
}