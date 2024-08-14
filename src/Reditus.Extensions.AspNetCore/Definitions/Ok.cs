using System.Net;
using Reditus.Abstractions;
using Reditus.Definitions;
using Reditus.Extensions.AspNetCore.Abstractions;

namespace Reditus.Extensions.AspNetCore.Definitions;

public class Ok<T> : Success<T>, IHttpSuccess<T>
{
    public HttpStatusCode HttpStatusCode => HttpStatusCode.OK;

    public Ok(T value) : base(value)
    {
    }
}