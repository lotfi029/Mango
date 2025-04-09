using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Abstracts;

public static class ProblemDetailsExtention
{
    public static IResult ToProblem(this Result result)
    {
        if (result.IsSucceed)
            throw new ArgumentException("invalid probem details");

        var problem = TypedResults.Problem(statusCode: result.Error.StatusCode);

        var problemDetails = problem.GetType().GetProperty(nameof(ProblemDetails))!.GetValue(problem) as ProblemDetails;

        problemDetails!.Extensions = new Dictionary<string, object?>()
                        {
                            {
                                "Errors",  new[]
                                {
                                    result.Error.Code,
                                    result.Error.Description
                                }
                            }
                        };
        return TypedResults.Problem(problemDetails);
    }
}
