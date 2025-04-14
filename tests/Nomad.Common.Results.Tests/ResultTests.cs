namespace Nomad.Common.Results.Tests;

public class ResultTests
{
    [Fact]
    public void Success_Result_Should_Have_Succeeded_True_And_Failed_False()
    {
        var result = Result.Success();

        Assert.True(result.Succeeded);
        Assert.False(result.Failed);
        Assert.Null(result.FailureReason);
    }

    [Fact]
    public void Failure_Result_Should_Have_Succeeded_False_And_Failed_True()
    {
        var error = Error.Validation("Validation failed", "validation_error");
        var result = Result.Failure(error);

        Assert.False(result.Succeeded);
        Assert.True(result.Failed);
        Assert.Equal(error, result.FailureReason);
    }

    [Fact]
    public void Success_ResultT_Should_Have_Value_And_Succeeded_True()
    {
        var result = Result<string>.Success("hello");

        Assert.True(result.Succeeded);
        Assert.False(result.Failed);
        Assert.Equal("hello", result.Value);
        Assert.Null(result.FailureReason);
    }

    [Fact]
    public void Failure_ResultT_Should_Have_Error_And_Value_Null()
    {
        var error = Error.Conflict("Already exists", "conflict");
        var result = Result<string>.Failure(error);

        Assert.False(result.Succeeded);
        Assert.True(result.Failed);
        Assert.Null(result.Value);
        Assert.Equal(error, result.FailureReason);
    }

    [Fact]
    public void IResult_Contract_Should_Be_Respected_By_Result()
    {
        var result = Result.Failure(Error.NotFound("Not found"));

        Assert.False(result.Succeeded);
        Assert.True(result.Failed);
        Assert.Equal(ErrorType.NotFound, result.FailureReason?.Type);
    }

    [Fact]
    public void IResult_Contract_Should_Be_Respected_By_ResultT()
    {
        var result = Result<int>.Failure(Error.Internal("Oops"));

        Assert.False(result.Succeeded);
        Assert.True(result.Failed);
        Assert.Equal(ErrorType.Internal, result.FailureReason?.Type);
    }

    public Result<int> GetIntVal()
    {
        return Error.Conflict("sd");
    }
}
