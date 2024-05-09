# Reditus

[![NuGet Version](https://img.shields.io/nuget/v/Reditus)](https://www.nuget.org/packages/Reditus)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Reditus)](https://www.nuget.org/packages/Reditus)

Reditus, is a Result pattern library for every .NET application.

## Getting Started

You can install [Reditus with NuGet](https://www.nuget.org/packages/Reditus):

```text
 Install-Package Reditus
```

### Features

- **Versatile** — Can be used in any .NET project.
- **Immutable** — Once a Result is created, it cannot be changed.
- **Detailed on Failure** — A Result, when failed, contains a specific IError class.
- **Thread safe** — Results are immutable and by nature, safe to work with in multithreaded scenarios.
- **Extensible** — Extend the Error class by introducing your very own Error classes.
- **Fully tested** — The code has full coverage.

## Usage

Result objects can hold values or simply be used as flow state control. The values can be anything. A class, a
value-type, a struct, anything.

### Creating a Result

Typically, the `Result` class is being used by methods that don't return a value.

```csharp
var result = Result.Success();

var error = new Error("An error occured.");
var result = Result.Fail(error);

// the error can also hold an exception
var error = new Error("An error occured.", new Exception());
var result = Result.Fail(error);
```

An example usage of the `Result` class.

```csharp
public async Task<Result> ExecuteJob()
{
    try
    {
        var jobId = ExecuteCleanupJob();

        if (jobId == 0)
        {
            // create an Error indicating the reason of failure
            var error = new Error("Cleanup job was not executed.");

            return Result.Fail(error);
        }

        return Result.Success();
    }
    catch (Exception ex)
    {
        // create an Error and attach the exception
        var error = new Error("An unexpected error occured while trying execute Cleanup job.", ex);

        return Result.Fail(error);
    }
}
```

The `Result<T>` class is being used by methods that return a value.

```csharp
var result = Result<int>.Success(1);

var error = new Error("An error occured.");
var result = Result<int>.Fail(error);

// the error can also hold an exception
var error = new Error("An error occured.", new Exception());
var result = Result<int>.Fail(error);
```

An example usage of the `Result<T>` class.

```csharp
public async Task<Result<int>> ExecuteJob()
{
    try
    {
        var jobId = ExecuteCleanupJob();

        if (jobId == 0)
        {
            // create an Error indicating the reason of failure
            var error = new Error("Cleanup job was not executed.");

            return Result<int>.Fail(error);
        }

        return Result<int>.Success(jobId);
    }
    catch (Exception ex)
    {
        // create an Error and attach the exception
        var error = new Error("An unexpected error occured while trying execute Cleanup job.", ex);

        return Result<int>.Fail(error);
    }
}
```

### The anatomy of a Result

A `Result` holds certain information about itself.

```csharp
var result = Result.Success();

result.IsSuccessful // true
result.IsFailed // false
result.Error // throws InvalidOperationException as the result is not in a failed state


var result = Result.Fail(new Error("Operation failed.");

result.IsSuccessful // false
result.IsFailed // true
result.Error // IError instance
```

When the `Result<T>` holds a return value.

```csharp
var result = Result<int>.Success(1);

result.IsSuccessful // true
result.IsFailed // false
result.Value // 1
result.Error // throws InvalidOperationException as the result is not in a fail state


var result = Result<int>.Fail(new Error("Operation failed.");

result.IsSuccessful // false
result.IsFailed // true
result.Value // throws InvalidOperationException as the result is not in a success state
result.Error // IError instance
```

### Extending

You can introduce your very own Error classes by extending the existing one.

The below custom `NotFoundError` class is being used when an application might need to return a NotFound 404 response.

```csharp
public interface IHttpError : IError
{
    public HttpStatusCode HttpStatusCode { get; }
}

public sealed class NotFoundError : Error, IHttpError
{
    public HttpStatusCode HttpStatusCode => HttpStatusCode.NotFound;

    public NotFoundError(string message = "The request resource was not found.")
        : base(message)
    {
    }
}
```

An example of the above custom `Error` class.

```csharp
public async Task<Result<IEnumerable<Project>>> GetProjects()
{
    try
    {
        var projects = await GetProjects();

        if (!projects.Any())
        {
            var error = new NotFoundError(); // <-- the new NotFoundError Error class

            return Result<IEnumerable<Project>>.Fail(error);
        }

        return Result<IEnumerable<Project>>.Success(jobId);
    }
    catch (Exception ex)
    {
        // create an Error and attach the exception
        var error = new Error("An unexpected error occured while trying execute Cleanup job.", ex);

        return Result<IEnumerable<Project>>.Fail(error);
    }
}
```

You can also introduce simple `Error` classes to better reflect your domain.

```csharp
public sealed class ApplicationError : Error
{
    public ApplicationError(string eventId, string message, Exception exceptio)
        : base(message, exception)
    {
    }
}
```

The `Error` class provides 2 constructors, so you are free to use whichever suits your needs
best. [See definition](src/Reditus.Definitions/Error.cs)

