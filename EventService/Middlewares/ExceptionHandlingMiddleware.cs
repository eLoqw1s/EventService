using EventService.Exceptions;
using EventService.Models.DTO.Error;

namespace EventService.Middlewares
{
	public class ExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next,
			ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
			_logger = logger;
        }

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (CustomException ex)
			{
				await HandleExceptionAsync(httpContext, ex);
			}
			catch (Exception ex)
			{
				await HandleGeneralExceptionAsync(httpContext, ex);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context, CustomException ex)
		{
			_logger.LogError(ex.Message);

			HttpResponse response = context.Response;

			response.ContentType = "application/json";
			response.StatusCode = ex.ErrorCode;

			ErrorDto errorDto = new ErrorDto(
				ex.ErrorCode,
				ex.Message);

			await response.WriteAsJsonAsync(errorDto);
		}

		private Task HandleGeneralExceptionAsync(HttpContext context, Exception ex)
		{
			_logger.LogError(ex.Message);

			HttpResponse response = context.Response;

			response.ContentType = "application/json";

			var responseDto = new
			{
				message = "Something went wrong.",
				detailed = ex.Message
			};

			return response.WriteAsJsonAsync(responseDto);
		}
	}
}
