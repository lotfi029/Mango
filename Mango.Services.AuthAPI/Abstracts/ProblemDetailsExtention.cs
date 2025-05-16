using Microsoft.AspNetCore.Mvc;
using Store.Abstractions.Abstraction;

namespace Store.Services.AuthAPI.Abstracts;

public static class ProblemDetailsExtention
{
    public static IResult ToProblem(this Result result)
    {
        if (result.IsSuccess)
            throw new ArgumentException("invalid probem details");

        var problem = TypedResults.Problem(statusCode: result.Error.Code);

        var problemDetails = problem.GetType().GetProperty(nameof(ProblemDetails))!.GetValue(problem) as ProblemDetails;

        problemDetails!.Extensions = new Dictionary<string, object?>()
                        {
                            {
                                "Errors",  new[]
                                {
                                    result.Error.Description
                                }
                            }
                        };
        return TypedResults.Problem(problemDetails);
    }
}
