using System.Net;
using Reditus.Abstractions;
using Reditus.Definitions;
using Reditus.Extensions.AspNetCore.Http.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Definitions;

public class Ok<T> : Success<T>, IHttpSuccess<T>
{
    public HttpStatusCode StatusCode => HttpStatusCode.OK;

    public Ok(T value) : base(value)
    {
    }
}