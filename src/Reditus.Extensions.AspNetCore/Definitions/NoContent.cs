using System.Net;
using Reditus.Definitions;
using Reditus.Extensions.AspNetCore.Abstractions;

namespace Reditus.Extensions.AspNetCore.Definitions;

public class NoContent : Success, IHttpSuccess
{
    public HttpStatusCode HttpStatusCode => HttpStatusCode.NoContent;
}