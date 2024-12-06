using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace WeatherAPI.Common.ExceptionProblemDetails;

public class ValidationExceptionProblemDetails : ProblemDetails
{
    public ValidationExceptionProblemDetails(ValidationException exception)
    {
        Title = "Command validation error";
        Status = StatusCodes.Status400BadRequest;
        Type = "https://somedomain/validation-error";
        Errors = exception.Errors.Select(x => x.ErrorMessage).ToList();
    }

    public List<string> Errors { get; }
}