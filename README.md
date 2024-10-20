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
- **Detailed on Failure** — A Result, when failed, contains a specific Error class.
- **Thread safe** — Results are immutable and by nature, safe to work with in multithreaded scenarios.
- **Extensible** — Extend the Result class or the Error class by introducing your very own classes.
- **Fully tested** — The code has full coverage.

## Usage
The `Result` object can be used as flow state control.

The `Result<T>` object can hold any value. A class, a
value-type, a struct, anything.

### Creating a Result

Typically, the `Result` class is being used by methods that don't return a value.

```csharp
var result = Result.CreateSuccess(); // creates a result in success state

var result = Result.CreatFail(); // creates a result in fail state

var error = new Error("An error occured."); // the error can also hold a message
var result = Result.CreatFail(error);

// the error can also hold an exception
var error = new Error("An error occured.", new Exception());
var result = Result.CreatFail(error);
```

An example usage of the `Result` class.

```csharp
public async Task<Result> ExecuteJob()
{
    try
    {
        var jobId = ExecuteJob();

        if (jobId == 0)
        {
            // create an Error indicating the reason of failure
            var error = new Error("Cleanup job was not executed.");

            return Result.CreateFail(error);
        }

        return Result.CreateSuccess();
    }
    catch (Exception ex)
    {
        // create an Error and attach the exception
        var error = new Error("An unexpected error occured while trying execute Cleanup job.", ex);

        return Result.CreateFail(error);
    }
}
```

The `Result` class also supports the implicit operator.

```csharp
public async Task<Result> ExecuteJob()
{
    try
    {
        var jobId = ExecuteJob();

        if (jobId == 0)
        {
            // create an Error indicating the reason of failure
            var error = new Error("Cleanup job was not executed.");

            return error; // this implicitly is being converted into Result.CreateFail(error);
        }

        return Result.CreateSuccess(); // no implicit operator can be used since there is not value
    }
    catch (Exception ex)
    {
        // create an Error and attach the exception
        var error = new Error("An unexpected error occured while trying execute Cleanup job.", ex);

        return error;
    }
}
```

The `Result<T>` class is being used by methods that return a value.

```csharp
var result = Result<int>.CreateSuccess(1); // creates a result in success state

var result = Result<int>.CreateFail(); // creates a result in fail state

// the error can also hold a message
var error = new Error("An error occured.");
var result = Result.CreateFail(error);

// the error can also hold an exception
var error = new Error("An error occured.", new Exception());
var result = Result<int>.CreateFail(error);
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

            return Result<int>.CreateFail(error);
        }

        return Result<int>.CreateSuccess(jobId);
    }
    catch (Exception ex)
    {
        // create an Error and attach the exception
        var error = new Error("An unexpected error occured while trying execute Cleanup job.", ex);

        return Result<int>.CreateFail(error);
    }
}
```

The `Result<T>` class also supports the implicit operator.

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

            return error; // this implicitly is being converted into Result<int>.CreateFail(error);
        }

        return jobId; // this implicitly is being converted into Result<int>.CreateSuccess(error);
    }
    catch (Exception ex)
    {
        // create an Error and attach the exception
        var error = new Error("An unexpected error occured while trying execute Cleanup job.", ex);

        return error;
    }
}
```

### The anatomy of a Result

A `Result` holds certain information about itself.

```csharp
var result = Result.CreateSuccess();

result.IsSuccessful // true
result.IsFailed // false
result.Error // throws InvalidOperationException as the result is not in a failed state


var result = Result.CreateFail();

result.IsSuccessful // false
result.IsFailed // true
result.Error // Error instance
```

When the `Result<T>` holds a return value.

```csharp
var result = Result<int>.CreateSuccess(1);

result.IsSuccessful // true
result.IsFailed // false
result.Value // 1
result.Error // throws InvalidOperationException as the result is not in a fail state


var result = Result<int>.CreateFail();

result.IsSuccessful // false
result.IsFailed // true
result.Value // throws InvalidOperationException as the result is not in a success state
result.Error // IError instance
```

### Extending

You can introduce your very own Error classes by extending the existing one.

The below custom `NotFoundError` class is being used when an application might need to return a NotFound 404 response.

```csharp
public interface ICustomError : IError
{
    public HttpStatusCode HttpStatusCode { get; }
}

public sealed class NotFoundError : Error, ICustomError
{
    public HttpStatusCode HttpStatusCode => HttpStatusCode.NotFound;

    public NotFoundError(string message)
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
            var error = new NotFoundError("The request resource was not found."); // <-- the new NotFoundError Error class

            return error;
        }

        return jobId;
    }
    catch (Exception ex)
    {
        // create an Error and attach the exception
        var error = new Error("An unexpected error occured while trying execute Cleanup job.", ex);

        return error;
    }
}
```

The `Error` class provides many constructors, so you are free to use whichever suits your needs
best. [See definition](src/Reditus/Definitions/Error.cs)

