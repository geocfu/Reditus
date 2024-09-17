using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Reditus.Extensions.AspNetCore.Http.Abstractions;

/// <summary>
/// Defines a contract for converting <see cref="HttpResult"/> objects to <see cref="ActionResult"/> objects
/// for ASP.NET Core MVC actions.
/// </summary>
public interface IActionResultConverter
{
    /// <summary>
    /// Converts a <see cref="HttpResult{T}"/> to an <see cref="ActionResult{T}"/> for use in an API response.
    /// </summary>
    /// <typeparam name="T">The type of the data contained in the <see cref="HttpResult{T}"/>.</typeparam>
    /// <param name="httpResult">The result object that contains data and status for the HTTP response.</param>
    /// <returns>An <see cref="ActionResult{T}"/> that contains the appropriate HTTP response and data.</returns>
    public ActionResult<T> ToActionResult<T>(HttpResult<T> httpResult);

    /// <summary>
    /// Asynchronously converts a <see>
    ///     <cref>Task{HttpResult{T}}</cref>
    /// </see>
    /// to an <see cref="ActionResult{T}"/> for use in an API response.
    /// </summary>
    /// <typeparam name="T">The type of the data contained in the <see cref="HttpResult{T}"/>.</typeparam>
    /// <param name="httpResultTask">The task that resolves to the <see cref="HttpResult{T}"/>.</param>
    /// <returns>A task representing the conversion, resolving to an <see cref="ActionResult{T}"/>.</returns>
    public Task<ActionResult<T>> ToActionResult<T>(Task<HttpResult<T>> httpResultTask);

    /// <summary>
    /// Converts a non-generic <see cref="HttpResult"/> to an <see cref="ActionResult"/> for use in an API response.
    /// </summary>
    /// <param name="httpResult">The result object that contains status information for the HTTP response.</param>
    /// <returns>An <see cref="ActionResult"/> that represents the appropriate HTTP response.</returns>
    public ActionResult ToActionResult(HttpResult httpResult);

    /// <summary>
    /// Asynchronously converts a <see cref="Task{HttpResult}"/> to an <see cref="ActionResult"/> for use in an API response.
    /// </summary>
    /// <param name="httpResultTask">The task that resolves to the <see cref="HttpResult"/>.</param>
    /// <returns>A task representing the conversion, resolving to an <see cref="ActionResult"/>.</returns>
    public Task<ActionResult> ToActionResult(Task<HttpResult> httpResultTask);
}