using BenchmarkDotNet.Attributes;
using Reditus.Core;
using Reditus.Definitions;

namespace Reditus.Benchmarks.Cases;

[MemoryDiagnoser]
public class ResultCase
{
    // [Benchmark]
    // public Result Successful_With_No_Return_Value()
    // {
    //     return Result.CreateSuccess();
    // }
    //
    // [Benchmark]
    // public Result<string> Successful_With_Return_Value_String()
    // {
    //     return Result<string>.Success("This is a successful result.");
    // }
    //
    // [Benchmark]
    // public Result<int> Successful_With_Return_Value_Integer()
    // {
    //     return Result<int>.Success(100);
    // }
    //
    // [Benchmark]
    // public Result Failed_With_No_Return_Value()
    // {
    //     return Result.Fail(new Failure("An failure occured"));
    // }
    //
    // [Benchmark]
    // public Result Failed_With_No_Return_Value_And_Exception()
    // {
    //     return Result.Fail(new Failure("An failure occured", new Exception("An exception occured.")));
    // }
    //
    // [Benchmark]
    // public Result<object> Failed_With_Return_Value()
    // {
    //     return Result<object>.Fail(new Failure("An failure occured"));
    // }
    //
    // [Benchmark]
    // public Result<object> Failed_WithReturn_Value_And_Exception()
    // {
    //     return Result<object>.Fail(new Failure("An failure occured", new Exception("An exception occured.")));
    // }
}