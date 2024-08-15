using System.Net;
using Reditus.Definitions;
using Reditus.Extensions.AspNetCore.Http.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Definitions;

public class Created<T> : Success<T>, IHttpSuccess<T>
{
    public HttpStatusCode StatusCode => HttpStatusCode.Created;

    public Created(T value) : base(value)
    {
    }
}