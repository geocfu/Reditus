using System.Net;
using Reditus.Abstractions;

namespace Reditus.Extensions.AspNetCore.Abstractions;

public interface IHttpSuccess<out T> : ISuccess<T>
{
    /// <summary>
    ///
    /// </summary>
    HttpStatusCode HttpStatusCode { get; }
}