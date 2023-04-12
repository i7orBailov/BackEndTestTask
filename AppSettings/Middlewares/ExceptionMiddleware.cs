using Serilog;
using Newtonsoft.Json;
using Serilog.Context;
using BackEndTestTask.Models;
using Serilog.Formatting.Json;
using BackEndTestTask.Models.Database;
using BackEndTestTask.Models.Enums;
using BackEndTestTask.Models.Repositories.Interfaces;

namespace BackEndTestTask.AppSettings.Middlewares
{
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
                SaveToFileExceptionJournal(journalRecord);

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

        private void SaveToFileExceptionJournal(ExceptionJournal record)
        {
            var logger = new LoggerConfiguration()
                .WriteTo.File(new JsonFormatter(), GetLogFilePath(), rollingInterval: RollingInterval.Day)
                .CreateLogger();
            using (LogContext.PushProperty(nameof(ExceptionJournal), record, destructureObjects: true))
            {
                var eventName = "{@0}".Replace("0", nameof(ExceptionJournal));
                logger.Error(eventName, record);
            }
        }

        private string GetLogFilePath()
        {
            string logFolderPath = Path.Combine(Environment.CurrentDirectory, "logs");
            if (!Directory.Exists(logFolderPath))
            {
                Directory.CreateDirectory(logFolderPath);
            }
            return Path.Combine(logFolderPath, $"{DateTime.UtcNow:yyyy-MM-dd}.log");
        }
    }
}
