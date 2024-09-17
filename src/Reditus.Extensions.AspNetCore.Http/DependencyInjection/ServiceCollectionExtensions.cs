using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Reditus.Extensions.AspNetCore.Http.Abstractions;
using Reditus.Extensions.AspNetCore.Http.Converters;

namespace Reditus.Extensions.AspNetCore.Http.DependencyInjection;

/// <summary>
/// Provides extension methods for configuring services related to HTTP results in ASP.NET Core.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds services for handling HTTP results to the dependency injection container.
    /// </summary>
    /// <param name="services">The collection of services to configure.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddReditusHttpResult(this IServiceCollection services)
    {
        // Register IActionContextAccessor as a singleton service if it's not already registered.
        services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

        // Register ActionResultConverter as a scoped service.
        services.AddScoped<IActionResultConverter, ActionResultConverter>();

        return services;
    }
}