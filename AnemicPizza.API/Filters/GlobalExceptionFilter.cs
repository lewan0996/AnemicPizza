using System.ComponentModel.DataAnnotations;
using AnemicPizza.Core.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
#pragma warning disable 1591

namespace AnemicPizza.API.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(IWebHostEnvironment environment, ILogger<GlobalExceptionFilter> logger)
        {
            _environment = environment;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
			_logger.LogError(new EventId(context.Exception.HResult), context.Exception, context.Exception.Message);

            if (context.Exception is DomainException || context.Exception is ValidationException)
            {
                var problemDetails = new ValidationProblemDetails
                {
                    Instance = context.HttpContext.Request.Path,
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Please refer to the errors property for additional details."
                };

                context.Result = new BadRequestObjectResult(problemDetails);
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                switch (context.Exception)
                {
                    case DomainException _:
                        problemDetails.Errors.Add("DomainValidations", new[] { context.Exception.Message });
                        break;
                    case ValidationException exception:
                        problemDetails.Errors.Add("CommandValidations",
                            new[] {exception.ValidationResult.ErrorMessage});
                        break;
                }
            }
            else if (context.Exception is RecordNotFoundException ex)
            {
                var json = new JsonErrorResponse { Messages = new[] { ex.Message } };

                context.Result = new NotFoundObjectResult(json);
                context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            else
            {
                var json = new JsonErrorResponse { Messages = new[] { "An error occur.Try it again." } };

                if (_environment.IsDevelopment())
                {
                    json.DeveloperMessage = context.Exception;
                }

                context.Result = new ObjectResult(json) {StatusCode = 500};
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
        }
	}

    internal class JsonErrorResponse
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string[] Messages { get; set; }
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public object DeveloperMessage { get; set; }
    }
}
