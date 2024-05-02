using Reditus.Definitions;

namespace Reditus.Benchmarks.Cases;

using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
public class ResultCase
{
    [Benchmark]
    public Result Successful_With_No_Return_Value()
    {
        return Result.Successful();
    }

    // [Benchmark]
    // public Result Successful_With_Return_Value_String()
    // {
    //     return Result<string>.Successful("This is a successful result.");
    // }
    //
    // [Benchmark]
    // public Result Successful_With_Return_Value_Integer()
    // {
    //     return Result<int>.Successful(100);
    // }
    //
    // [Benchmark]
    // public Result Failed_With_No_Return_Value()
    // {
    //     return Result.Failed(new Error("An error occured"));
    // }
    //
    // [Benchmark]
    // public Result Failed_With_No_Return_Value_And_Metadata()
    // {
    //     return Result.Failed(new Error("An error occured", new Dictionary<string, object>
    //     {
    //         { "key", "value" }
    //     }));
    // }
    //
    // [Benchmark]
    // public Result Failed_With_No_Return_Value_And_Exception()
    // {
    //     return Result.Failed(new Error("An error occured", new Exception("An exception occured.")));
    // }
    //
    // [Benchmark]
    // public Result Failed_With_No_Return_Value_And_Exception_And_Metadata()
    // {
    //     return Result.Failed(new Error("An error occured",
    //         new Exception("An exception occured."),
    //         new Dictionary<string, object>
    //         {
    //             { "key", "value" }
    //         }));
    // }
    //
    // [Benchmark]
    // public Result Failed_With_Return_Value()
    // {
    //     return Result<object>.Failed(new Error("An error occured"));
    // }
    //
    // [Benchmark]
    // public Result Failed_With_Return_Value_And_Metadata()
    // {
    //     return Result<object>.Failed(new Error("An error occured", new Dictionary<string, object>
    //     {
    //         { "key", "value" }
    //     }));
    // }
    //
    // [Benchmark]
    // public Result Failed_WithReturn_Value_And_Exception()
    // {
    //     return Result<object>.Failed(new Error("An error occured", new Exception("An exception occured.")));
    // }
    //
    // [Benchmark]
    // public Result Failed_With_Return_Value_And_Exception_And_Metadata()
    // {
    //     return Result<object>.Failed(new Error("An error occured",
    //         new Exception("An exception occured."),
    //         new Dictionary<string, object>
    //         {
    //             { "key", "value" }
    //         }));
    // }
}