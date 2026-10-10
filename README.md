# Reditus

**Stop throwing exceptions for things you expected to happen.**

Reditus is a small, dependency-free Result pattern library for .NET. Your methods return a `Result` that says
whether they succeeded, and why they did not.

[![NuGet Version](https://img.shields.io/nuget/v/Reditus)](https://www.nuget.org/packages/Reditus)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Reditus)](https://www.nuget.org/packages/Reditus)
[![License](https://img.shields.io/github/license/geocfu/Reditus)](https://github.com/geocfu/Reditus/blob/main/LICENSE)

```csharp
public Result<User> FindUser(int id)
{
    var user = _users.Find(id);

    if (user is null)
    {
        return new Error($"User {id} was not found."); // a failed result
    }

    return user; // a successful result
}
```

## Why Reditus

|                         |                                                                                      |
|-------------------------|--------------------------------------------------------------------------------------|
| **Honest signatures**   | A method returning `Result<User>` tells its caller it can fail. No hidden exceptions. |
| **Clean code**          | Return a value or an `Error` directly. Reditus converts it into a result for you.     |
| **Detailed failures**   | Every failed result carries an `Error` with a message and, optionally, the exception. |
| **Immutable**           | A result never changes after it is created, so it is safe to share across threads.    |
| **Extensible**          | Derive your own error types, such as `NotFoundError` or `ValidationError`.            |
| **Runs everywhere**     | .NET Standard 2.0 and 2.1, and .NET 5 through .NET 10. No dependencies.               |

## Install

```text
dotnet add package Reditus
```

Or get it from [NuGet](https://www.nuget.org/packages/Reditus).

## Quick start

Add the two namespaces:

```csharp
using Reditus;
using Reditus.Definitions; // Error
```

Return a result from your method:

```csharp
public Result<int> Divide(int dividend, int divisor)
{
    if (divisor == 0)
    {
        return new Error("Cannot divide by zero.");
    }

    return dividend / divisor;
}
```

Then check it where you call the method:

```csharp
var result = Divide(10, 2);

if (result.IsFailed)
{
    Console.WriteLine(result.Error.Message);
    return;
}

Console.WriteLine(result.Value); // 5
```

That is the whole pattern. The rest of this page is detail.

## Usage

### Two kinds of result

| Type        | Use it when the method...          | Example                         |
|-------------|------------------------------------|---------------------------------|
| `Result`    | returns nothing, but can fail      | `Result SendEmail(...)`         |
| `Result<T>` | returns a value, and can fail      | `Result<Order> GetOrder(...)`   |

`T` can be anything: a class, a struct, a value type.

### Creating a result

```csharp
// Result
Result ok = Result.CreateSuccess();
Result failed = Result.CreateFail(); // default message: "An error occured."
Result failedWithReason = Result.CreateFail(new Error("Job was not executed."));

// Result<T>
Result<int> okWithValue = Result<int>.CreateSuccess(1);
Result<int> failedValue = Result<int>.CreateFail(new Error("Job was not executed."));
```

Or skip the factory methods and let the implicit conversion do the work:

```csharp
public Result<int> ExecuteJob()
{
    try
    {
        var jobId = RunCleanup();

        if (jobId == 0)
        {
            return new Error("Cleanup job was not executed."); // same as Result<int>.CreateFail(error)
        }

        return jobId; // same as Result<int>.CreateSuccess(jobId)
    }
    catch (Exception ex)
    {
        return new Error("An unexpected error occured while executing the cleanup job.", ex);
    }
}
```

A non-generic `Result` has no value to convert from, so its success is always `Result.CreateSuccess()`. Its failure
can still be returned as a plain `Error`.

### Reading a result

| Member         | Successful result                  | Failed result                      |
|----------------|------------------------------------|------------------------------------|
| `IsSuccessful` | `true`                             | `false`                            |
| `IsFailed`     | `false`                            | `true`                             |
| `Value`        | the value (`Result<T>` only)       | throws `InvalidOperationException` |
| `Error`        | throws `InvalidOperationException` | the `Error`                        |

Check `IsSuccessful` or `IsFailed` before you read `Value` or `Error`.

### Errors

An `Error` holds a `Message` and an optional `Exception`:

```csharp
new Error(); // message defaults to "An error occured."
new Error("Payment was declined.");
new Error(exception);
new Error("Payment was declined.", exception);
```

### Passing a failure up the call chain

When a method you call fails, hand the same error to your own caller, even if the value types differ:

```csharp
public Result<Invoice> CreateInvoice(int orderId)
{
    Result<Order> order = GetOrder(orderId);

    if (order.IsFailed)
    {
        return Result<Invoice>.CreateFail(order); // keeps the original Error
    }

    return new Invoice(order.Value);
}
```

### Your own error types

Derive from `Error` to give failures a meaning your application can act on, for example an HTTP status code:

```csharp
public sealed class NotFoundError : Error
{
    public HttpStatusCode HttpStatusCode => HttpStatusCode.NotFound;

    public NotFoundError(string message)
        : base(message)
    {
    }
}
```

Return it like any other error, and match on it at the edge of your application:

```csharp
public Result<Project> GetProject(int id)
{
    var project = _projects.Find(id);

    if (project is null)
    {
        return new NotFoundError($"Project {id} was not found.");
    }

    return project;
}
```

```csharp
var result = GetProject(id);

if (result.IsFailed)
{
    return result.Error is NotFoundError ? NotFound() : Problem(result.Error.Message);
}

return Ok(result.Value);
```

## Good to know

- **A successful `Result<T>` cannot hold `null`.** Creating one with a `null` value throws `ArgumentNullException`.
  Return an `Error` instead.
- **Interface-typed values need the factory method.** C# does not allow implicit conversions from an interface, so
  a variable declared as `IEnumerable<Project>` will not convert to `Result<IEnumerable<Project>>` on its own. Use
  `Result<IEnumerable<Project>>.CreateSuccess(projects)`.

## License

Reditus is released under the [MIT License](https://github.com/geocfu/Reditus/blob/main/LICENSE).
