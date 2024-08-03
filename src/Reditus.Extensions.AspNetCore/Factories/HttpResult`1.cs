using Reditus.Core;
using Reditus.Extensions.AspNetCore.Definitions;

namespace Reditus.Extensions.AspNetCore.Factories;

public static class HttpResult<T>
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Result<T> CreateOk(T value)
    {
        var success = new Ok<T>(value);
        return new Result<T>(success);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Result<T> CreateCreated(T value)
    {
        var success = new Created<T>(value);
        return new Result<T>(success);
    }
}