using System.Net;
using Reditus.Definitions;
using Reditus.Extensions.AspNetCore.Http.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Definitions;

public class NoContent : Success, IHttpSuccess
{
    public HttpStatusCode StatusCode => HttpStatusCode.NoContent;
}