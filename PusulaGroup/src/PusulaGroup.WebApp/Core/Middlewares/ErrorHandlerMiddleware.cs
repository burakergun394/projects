using System.Net;
using System.Text.Json;

namespace PusulaGroup.WebApp.Core.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> logger;
        private readonly IWebHostEnvironment environtment;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger, IWebHostEnvironment environtment)
        {
            _next = next;
            this.logger = logger;
            this.environtment = environtment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                logger.LogError(error, "An error occured");
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = error switch
                {
                    _ => (int)HttpStatusCode.InternalServerError,
                };
                var errorMessage = environtment.IsDevelopment() ? error?.Message : "An error occured. Please try again";
                //var errorMessage =  error?.Message ;

                var result = JsonSerializer.Serialize(new { errorMessage });
                await response.WriteAsync(result);
            }
        }
    }
}
