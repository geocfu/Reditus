using System.Net;
using Reditus.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Abstractions;

public interface IHttpSuccess<out T> : ISuccess<T>
{
    HttpStatusCode StatusCode { get; }
}