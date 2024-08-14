using System.Collections.Generic;
using Reditus.Extensions.AspNetCore.Definitions;

namespace Reditus.Extensions.AspNetCore;

/// <summary>
///
/// </summary>
public static class HttpResult
{
    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public static Result CreateNoContent()
    {
        var noContent = new NoContent();
        return new Result(noContent);
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public static Result CreateBadRequest(IDictionary<string, string[]> validationErrors)
    {
        var badRequest = new BadRequest(validationErrors);
        return new Result(badRequest);
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public static Result CreateBadRequest(string message, IDictionary<string, string[]> validationErrors)
    {
        var badRequest = new BadRequest(message, validationErrors);
        return new Result(badRequest);
    }

}