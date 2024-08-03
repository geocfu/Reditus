using System.Net;
using Reditus.Abstractions;

namespace Reditus.Extensions.AspNetCore.Abstractions
{
    /// <summary>
    ///
    /// </summary>
    public interface IHttpFailure : IFailure
    {
        /// <summary>
        ///
        /// </summary>
        // public string Type { get; }

        /// <summary>
        ///
        /// </summary>
        // public string Title { get; }

        /// <summary>
        ///
        /// </summary>
        HttpStatusCode Status { get; }
    }
}