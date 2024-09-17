using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Reditus.Extensions.AspNetCore.Http.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Converters
{
    /// <summary>
    /// Converts <see cref="HttpResult"/> and its derived types to <see cref="ActionResult"/> objects
    /// for use in ASP.NET Core MVC actions.
    /// </summary>
    public class ActionResultConverter : IActionResultConverter
    {
        private readonly ProblemDetailsFactory _problemDetailsFactory;
        private readonly IActionContextAccessor _actionContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionResultConverter"/> class.
        /// </summary>
        /// <param name="problemDetailsFactory">Factory for creating <see cref="ProblemDetails"/> instances.</param>
        /// <param name="actionContextAccessor">Accessor for the current <see cref="ActionContext"/>.</param>
        public ActionResultConverter(
            ProblemDetailsFactory problemDetailsFactory,
            IActionContextAccessor actionContextAccessor)
        {
            _problemDetailsFactory = problemDetailsFactory;
            _actionContextAccessor = actionContextAccessor;
        }

        /// <summary>
        /// Converts a <see cref="HttpResult{T}"/> to an <see cref="ActionResult{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the result value.</typeparam>
        /// <param name="httpResult">The HTTP result to convert.</param>
        /// <returns>An <see cref="ActionResult{T}"/> representing the HTTP result.</returns>
        public ActionResult<T> ToActionResult<T>(HttpResult<T> httpResult)
        {
            ArgumentNullException.ThrowIfNull(httpResult);

            if (httpResult.IsSuccessful)
            {
                return CreateObjectResult(httpResult.Success);
            }

            return CreateProblemObjectResult(httpResult.Failure);
        }

        /// <summary>
        /// Asynchronously converts a <see cref="Task{HttpResult{T}}"/> to an <see cref="ActionResult{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the result value.</typeparam>
        /// <param name="httpResultTask">The task that resolves to the HTTP result.</param>
        /// <returns>A task representing the conversion, resolving to an <see cref="ActionResult{T}"/>.</returns>
        public async Task<ActionResult<T>> ToActionResult<T>(Task<HttpResult<T>> httpResultTask)
        {
            ArgumentNullException.ThrowIfNull(httpResultTask);

            var httpResult = await httpResultTask;

            if (httpResult.IsSuccessful)
            {
                return CreateObjectResult(httpResult.Success);
            }

            return CreateProblemObjectResult(httpResult.Failure);
        }

        /// <summary>
        /// Converts a non-generic <see cref="HttpResult"/> to an <see cref="ActionResult"/>.
        /// </summary>
        /// <param name="httpResult">The HTTP result to convert.</param>
        /// <returns>An <see cref="ActionResult"/> representing the HTTP result.</returns>
        public ActionResult ToActionResult(HttpResult httpResult)
        {
            ArgumentNullException.ThrowIfNull(httpResult);

            if (httpResult.IsSuccessful)
            {
                return CreateObjectResult(httpResult.Success);
            }

            return CreateProblemObjectResult(httpResult.Failure);
        }

        /// <summary>
        /// Asynchronously converts a <see cref="Task{HttpResult}"/> to an <see cref="ActionResult"/>.
        /// </summary>
        /// <param name="httpResultTask">The task that resolves to the HTTP result.</param>
        /// <returns>A task representing the conversion, resolving to an <see cref="ActionResult"/>.</returns>
        public async Task<ActionResult> ToActionResult(Task<HttpResult> httpResultTask)
        {
            ArgumentNullException.ThrowIfNull(httpResultTask);

            var httpResult = await httpResultTask;

            if (httpResult.IsSuccessful)
            {
                return CreateObjectResult(httpResult.Success);
            }

            return CreateProblemObjectResult(httpResult.Failure);
        }

        /// <summary>
        /// Creates an <see cref="ObjectResult"/> for a successful HTTP result with a value of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the result value.</typeparam>
        /// <param name="httpSuccess">The successful HTTP result containing the value.</param>
        /// <returns>An <see cref="ObjectResult"/> representing the successful result.</returns>
        private static ObjectResult CreateObjectResult<T>(IHttpSuccess<T> httpSuccess)
        {
            return new ObjectResult(httpSuccess.Value)
            {
                StatusCode = (int)httpSuccess.HttpStatusCode,
            };
        }

        /// <summary>
        /// Creates an <see cref="ObjectResult"/> for a successful HTTP result without a value.
        /// </summary>
        /// <param name="httpSuccess">The successful HTTP result.</param>
        /// <returns>An <see cref="ObjectResult"/> representing the successful result.</returns>
        private static ObjectResult CreateObjectResult(IHttpSuccess httpSuccess)
        {
            return new ObjectResult(null)
            {
                StatusCode = (int)httpSuccess.HttpStatusCode,
            };
        }

        /// <summary>
        /// Adds validation errors from a <see cref="ReadOnlyDictionary{String, String[]}"/> to the provided <see cref="ModelStateDictionary"/>.
        /// </summary>
        /// <param name="modelState">The model state to which errors will be added.</param>
        /// <param name="errors">The validation errors to add.</param>
        private static void AddToModelState(
            ModelStateDictionary modelState,
            ReadOnlyDictionary<string, string[]> errors)
        {
            foreach (var (key, keyErrors) in errors)
            {
                foreach (var keyError in keyErrors)
                {
                    modelState.AddModelError(key, keyError);
                }
            }
        }

        /// <summary>
        /// Creates an <see cref="ObjectResult"/> for an HTTP failure result, including validation errors if present.
        /// </summary>
        /// <param name="httpFailure">The HTTP failure result containing error details.</param>
        /// <returns>An <see cref="ObjectResult"/> representing the failure result.</returns>
        private ObjectResult CreateProblemObjectResult(IHttpFailure httpFailure)
        {
            ArgumentNullException.ThrowIfNull(_actionContextAccessor.ActionContext);

            if (httpFailure.Errors.Count > 0)
            {
                AddToModelState(_actionContextAccessor.ActionContext.ModelState, httpFailure.Errors);

                var validationProblemDetails =
                    _problemDetailsFactory.CreateValidationProblemDetails(
                        _actionContextAccessor.ActionContext.HttpContext,
                        _actionContextAccessor.ActionContext.ModelState,
                        (int)httpFailure.HttpStatusCode);

                return new ObjectResult(validationProblemDetails);
            }

            var problemDetails =
                _problemDetailsFactory.CreateProblemDetails(
                    _actionContextAccessor.ActionContext.HttpContext,
                    statusCode: (int)httpFailure.HttpStatusCode,
                    detail: httpFailure.Message);

            return new ObjectResult(problemDetails);
        }
    }
}