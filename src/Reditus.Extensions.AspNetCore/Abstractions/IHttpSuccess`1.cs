using System.Net;
using Reditus.Abstractions;

namespace Reditus.Extensions.AspNetCore.Abstractions;

public interface IAspNetCoreSuccess<T> : ISuccess<T>
{
    /// <summary>
    ///
    /// </summary>
    HttpStatusCode HttpStatusCode { get; }
}