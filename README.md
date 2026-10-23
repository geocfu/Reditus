# Reditus

**Stop throwing exceptions for things you expected to happen.**

[![NuGet Version](https://img.shields.io/nuget/v/Reditus)](https://www.nuget.org/packages/Reditus)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Reditus)](https://www.nuget.org/packages/Reditus)
[![Coverage](https://codecov.io/gh/geocfu/Reditus/graph/badge.svg)](https://codecov.io/gh/geocfu/Reditus)
[![License](https://img.shields.io/github/license/geocfu/Reditus)](https://github.com/geocfu/Reditus/blob/main/LICENSE)

[![Unit Tests](https://github.com/geocfu/Reditus/actions/workflows/unit_tests.yml/badge.svg?event=pull_request)](https://github.com/geocfu/Reditus/actions/workflows/unit_tests.yml?query=event%3Apull_request)
[![Compatibility Tests](https://github.com/geocfu/Reditus/actions/workflows/compatibility_tests.yml/badge.svg?event=pull_request)](https://github.com/geocfu/Reditus/actions/workflows/compatibility_tests.yml?query=event%3Apull_request)

Reditus is a small, dependency-free **Result pattern** library for .NET. Your methods return a `Result` that says
whether they succeeded and, if not, why. Failures become values you can see in the signature, pass along and
match on, instead of exceptions you have to remember to catch.

- 🔍 **Honest signatures**: a method returning `Result<User>` tells its caller it can fail. No hidden exceptions.
- 🧾 **Detailed failures**: every failed result carries an `Error` with a message and, optionally, the exception.
- 🛑 **Cancellation-aware**: tell a canceled operation apart from a real failure with `OperationCanceledError`.
- 🧩 **Extensible**: derive your own error types, such as `NotFoundError` or `ValidationError`.
- 🔒 **Immutable**: a result never changes after it is created, so it is safe to share across threads.
- 🌍 **Runs everywhere**: .NET Standard 2.0 and 2.1, and .NET 5 through .NET 10. Zero dependencies.
- ✅ **Fully tested**: 100% line and branch coverage, enforced on every pull request into `main` and `develop`.

```csharp
public async Task<Result<User>> FindUserAsync(int id, CancellationToken cancellationToken)
{
    try
    {
        var user = await _users.FindAsync(id, cancellationToken);

        if (user is null)
        {
            return Result<User>.Failure(new NotFoundError($"User {id} was not found."));
        }

        return Result<User>.Success(user);
    }
    catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
    {
        return Result<User>.Failure(new OperationCanceledError());
    }
    catch (Exception exception)
    {
        return Result<User>.Failure(new Error("Could not load the user.", exception));
    }
}
```

`NotFoundError` is not part of Reditus; it is an error type you write in a few lines. See
[Your own error types](#your-own-error-types).

## Contents

- [Why Reditus](#why-reditus)
- [Install](#install)
- [Quick start](#quick-start)
- [Usage](#usage)
  - [Two kinds of result](#two-kinds-of-result)
  - [Creating a result](#creating-a-result)
  - [Reading a result](#reading-a-result)
  - [Errors](#errors)
  - [Passing a failure up the call chain](#passing-a-failure-up-the-call-chain)
  - [Your own error types](#your-own-error-types)
  - [Cancellation](#cancellation)
- [Good to know](#good-to-know)
- [Upgrading from 2.x](#upgrading-from-2x)
- [Contributing](#contributing)
- [License](#license)

## Why Reditus

An exception is the right tool for something that should never happen. For a user that does not exist or a
payment that was declined, it is a poor one: the signature hides it, every caller has to remember to catch it,
and every throw pays for a stack trace.

**Before**: the failure is invisible until it is thrown.

```csharp
public User FindUser(int id)
{
    return _users.Find(id) ?? throw new UserNotFoundException(id);
}
```

**After**: the failure is part of the contract.

```csharp
public Result<User> FindUser(int id)
{
    var user = _users.Find(id);

    return user is null
        ? Result<User>.Failure(new NotFoundError($"User {id} was not found."))
        : Result<User>.Success(user);
}
```

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
        return Result<int>.Failure(new Error("Cannot divide by zero."));
    }

    return Result<int>.Success(dividend / divisor);
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
Result ok = Result.Success();
Result failed = Result.Failure(); // default message: "An error occurred."
Result failedWithReason = Result.Failure(new Error("Job was not executed."));

// Result<T>
Result<int> okWithValue = Result<int>.Success(1);
Result<int> failedValue = Result<int>.Failure(new Error("Job was not executed."));
```

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
new Error(); // message defaults to "An error occurred."
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
        return Result<Invoice>.Failure(order); // keeps the original Error
    }

    return Result<Invoice>.Success(new Invoice(order.Value));
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
        return Result<Project>.Failure(new NotFoundError($"Project {id} was not found."));
    }

    return Result<Project>.Success(project);
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

### Cancellation

An `OperationCanceledError` tells callers that an operation was canceled, so they can tell a cancellation apart
from a failure. Catch the cancellation first, where it is thrown, and every other exception after it:

```csharp
public async Task<Result<Report>> GetReportAsync(int id, CancellationToken cancellationToken)
{
    try
    {
        var report = await _client.GetReportAsync(id, cancellationToken);
        return Result<Report>.Success(report);
    }
    catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
    {
        return Result<Report>.Failure(new OperationCanceledError());
    }
    catch (Exception exception)
    {
        return Result<Report>.Failure(new Error("Could not load the report.", exception));
    }
}
```

The `when` filter matters: an `HttpClient` timeout also throws an `OperationCanceledException` while your token was
not canceled. That is a real failure, so it falls through to the second `catch`. Use any error type you like there.
Pass the result up like any other failure.

In your own code, check the token instead of throwing:

```csharp
if (cancellationToken.IsCancellationRequested)
{
    return Result<Report>.Failure(new OperationCanceledError());
}
```

Match on it at the edge of your application:

```csharp
if (result.IsFailed)
{
    return result.Error is OperationCanceledError ? new EmptyResult() : Problem(result.Error.Message);
}
```

## Good to know

- **A successful `Result<T>` cannot hold `null`.** Creating one with a `null` value throws `ArgumentNullException`.
  Return `Result<T>.Failure(error)` instead.
- **An `Error` message cannot be empty.** A `null` message throws `ArgumentNullException`, and an empty or
  whitespace message throws `ArgumentException`.

## Upgrading from 2.x

The factory methods were renamed in 3.0:

| 2.x                  | 3.0                |
|----------------------|--------------------|
| `CreateSuccess(...)` | `Success(...)`     |
| `CreateFail(...)`    | `Failure(...)`     |

A blank `Error` message now throws `ArgumentException` instead of `ArgumentNullException`.

## Contributing

Issues and pull requests are welcome on [GitHub](https://github.com/geocfu/Reditus/issues). Before you open a
pull request, run the tests:

```text
dotnet test tests/Reditus.UnitTests /p:CollectCoverage=true /p:Threshold=100
dotnet test tests/Reditus.CompatibilityTests
```

Pull requests must keep line, branch and method coverage at 100%; the CI fails otherwise.

## License

Reditus is released under the [MIT License](https://github.com/geocfu/Reditus/blob/main/LICENSE).
