# Reditus: The Result pattern for .NET

Reditus, inspired by the Latin word for "reditus,", which means result, is a Result pattern library for every .net
application.

## Getting Started

`Install-Package Reditus`

### Features

- Can be used in any .net project.
- Immutable. Once a Result is created, it cannot be changed.
- Fully extensible. Create new Results or extend the Error functionality.
- Fully tested.

## Usage

Result objects can hold values or simply be used as flow state control. The values can be anything. A class, a
value-type, a struct, anything.

### Successful Result

Successful results can have a `value` or simply be used as flow state control.

```csharp
var result = Result.Successful(); // no return value

var value = 1;
var result = Result<int>.Successful(value); // with return value
```

### Failed Result

Failed results always contain an Error object indicating the reason of failure but can also indicate a return value on
successful operation.

```csharp
var error = new Error("An error occured.");
var result = Result.Failed(error); // simple error indicating reason of failure
var result = Result<int>.Failed(error); // indicating that the value should be int on successful operation


var exception = new Exception();
var result = Result.Failed(error, exception); // with an exception
var result = Result<int>.Failed(error, exception);

var metadata = new Dictionary<string, object> { { "key", "value" } };
var result = Result.Failed(error, metadata); // with metadata
var result = Result<int>.Failed(error, metadata);

var result = Result.Failed(error, exception, metadata); // with exception and metadata
var result = Result<int>.Failed(error, exception, metadata);
```

In a real world scenario, a Result object can be used as following.

```csharp
public async Task<Result<IEnumerable<ProjectDto>>> GetProjectsAsync(CancellationToken cancellationToken)
{
    try
    {
        // call a method that returns a Result
        Result<IEnumerable<Project>> result = await _projectRepository.GetProjectsAsync(cancellationToken);

        // check if the operation was successful
        if (!result.IsSuccessful) // or if (result.IsFailed)
        {
            // return a Result of different type by converting the previous
            return Result.Fail<IEnumerable<ProjectDto>>(result);
        }

        ProjectMapper projectMapper = new ProjectMapper();

        IEnumerable<ProjectDto>> projectDtos = result.Value.Select(p => projectMapper.ToProjectDto(p));

        return Result<IEnumerable<ProjectDto>>>.Ok(projectDtos);
    }
    catch (Exception ex)
    {
        _logger.Error(e, "An unexpected error occured while trying to retrieve the projects");

        // create an Error indicating the reason of failure and also attach the exception caused it
        Error error = new Error("An unexpected error occured while trying to retrieve projects.", ex);

        return Result<IEnumerable<ProjectDto>>.Fail(error);
    }
}
```

## Extending the Result