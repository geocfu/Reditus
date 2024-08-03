using System.Net;
using Reditus.Definitions;
using Reditus.Extensions.AspNetCore.Abstractions;

namespace Reditus.Extensions.AspNetCore.Definitions;

public class Created<T> : Success<T>, IAspNetCoreSuccess<T>
{
    public HttpStatusCode HttpStatusCode => HttpStatusCode.Created;

    public Created(T value) : base(value)
    {
    }
}