using System.Net;
using Reditus.Abstractions;

namespace Reditus.Extensions.AspNetCore.Abstractions;

public interface IHttpSuccess : ISuccess
{
    /// <summary>
    ///
    /// </summary>
    HttpStatusCode HttpStatusCode { get; }
}