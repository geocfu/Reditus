# Reditus

Reditus, is a Result pattern library for every .NET application.

## Getting Started

You can install Reditus with NuGet:

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
var result = Result.Successful();

var error = new Error("An error occured.");
var result = Result.Failed(error);

// the error can also hold an exception and/or metadata
var error = new Error("An error occured.", new Exception());
var error = new Error("An error occured.", new Dictionary<string, object> { { "EventId", "4576" } });
var error = new Error("An error occured.", new Exception(), new Dictionary<string, object> { { "EventId", "4576" } });
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

        return Result.Ok();
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
var result = Result<int>.Successful(1);

var error = new Error("An error occured.");
var result = Result<int>.Failed(error);

// the error can also hold an exception and/or metadata
var error = new Error("An error occured.", new Exception());
var error = new Error("An error occured.", new Dictionary<string, object> { { "EventId", "4576" } });
var error = new Error("An error occured.", new Exception(), new Dictionary<string, object> { { "EventId", "4576" } });
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

        return Result<int>.Ok(jobId);
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
var sucessfulResult = Result.Successful();

sucessfulResult.IsSuccessful // true
sucessfulResult.IsFailed // false
sucessfulResult.Error // throws InvalidOperationException as the result is not in a failed state


var failedResult = Result.Failed(new Error("Operation failed.");

failedResult.IsSuccessful // false
failedResult.IsFailed // true
failedResult.Error // IError instance
```

When the `Result<T>` holds a return value.

```csharp
var sucessfulResult = Result<int>.Successful(1);

sucessfulResult.IsSuccessful // true
sucessfulResult.IsFailed // false
sucessfulResult.Value // 1
sucessfulResult.Error // throws InvalidOperationException as the result is not in a fail state


var failedResult = Result<int>.Failed(new Error("Operation failed.");

failedResult.IsSuccessful // false
failedResult.IsFailed // true
failedResult.Value // throws InvalidOperationException as the result is not in a success state
failedResult.Error // IError instance
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

            return Result<int>.Fail(error);
        }

        return Result<int>.Ok(jobId);
    }
    catch (Exception ex)
    {
        // create an Error and attach the exception
        var error = new Error("An unexpected error occured while trying execute Cleanup job.", ex);

        return Result<int>.Fail(error);
    }
}
```

You can also introduce simple `Error` classes to better reflect your domain.

```csharp
public sealed class ApplicationError : Error
{
    public ApplicationError(string eventId, string message, Exception exception, Dictionary<string, object> metadata)
        : base(message, exception, metadata)
    {
    }
}
```

The `Error` class provides 3 constructors, so you are free to use whichever suits your needs
best. [See definition](src/Reditus.Definitions/Error.cs)

