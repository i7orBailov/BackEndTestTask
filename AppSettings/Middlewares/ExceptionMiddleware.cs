using Newtonsoft.Json;
using BackEndTestTask.Models;
using BackEndTestTask.Models.Database;
using BackEndTestTask.Models.Enums;
using BackEndTestTask.Models.Repositories.Interfaces;

namespace BackEndTestTask.AppSettings.Middlewares
{
    // TODO : Refactor using Seriolog because of possible leak if exception occurs in the database side

    //public class ExceptionMiddleware
    //{
    //    private readonly ILogger<ExceptionMiddleware> _logger;

    //    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    //    {
    //        _logger = logger;
    //    }

    //    public async Task InvokeAsync(HttpContext context)
    //    {
    //        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
    //        var exception = exceptionHandlerPathFeature.Error;

    //        var journalRecord = new ExceptionJournal
    //        {
    //            EventId = Guid.NewGuid(),
    //            Timestamp = DateTime.Now,
    //            QueryParams = context.Request.QueryString.ToString(),
    //            BodyParams = await new StreamReader(context.Request.Body).ReadToEndAsync(),
    //            StackTrace = exception.StackTrace
    //        };

    //        // Log journal record to file
    //        var options = new JsonSerializerOptions { WriteIndented = true };
    //        var jsonString = JsonSerializer.Serialize(journalRecord, options);
    //        _logger.LogError(jsonString);

    //        // Log exception details for debugging
    //        _logger.LogError(exception, "An error occurred while processing the request");

    //        // Return error response
    //        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
    //        await context.Response.WriteAsync("An error occurred while processing your request.");
    //    }
    //}
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                string stackTrace = ex.ToString();
                var repository = context.RequestServices.GetRequiredService<IBaseRepository<ExceptionJournal>>();
                var journalRecord = new ExceptionJournal
                {
                    EventId = Guid.NewGuid().ToString(),
                    Timestamp = DateTime.Now,
                    QueryParams = context.Request.QueryString.ToString(),
                    BodyParams = await new StreamReader(context.Request.Body).ReadToEndAsync(),
                    StackTrace = stackTrace
                };

                await repository.AddAsync(journalRecord);

                // Log exception details for debugging
                Console.WriteLine($"Exception {journalRecord.EventId} occurred at {journalRecord.Timestamp}: {stackTrace}");
                Console.WriteLine(journalRecord.StackTrace);

                // Return error response
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "text/plain";

                var responseContent = new ResponseMessageBase(isSuccessful: false, exception: new ExceptionTemplate
                (
                    evenId: journalRecord.EventId,
                    exceptionType: ex is SecureException ? ExceptionType.Secure : ExceptionType.Default,
                    exceptionMessage: ex is SecureException ? ex.Message : $"Internal server error ID = {journalRecord.EventId}"
                ));
                var responseContentJson = JsonConvert.SerializeObject(responseContent.ExceptionData);
                await context.Response.WriteAsync(responseContentJson);
            }
        }
    }
}
