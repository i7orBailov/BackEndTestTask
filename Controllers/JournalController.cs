using System.Net;
using BackEndTestTask.Models;
using BackEndTestTask.Helpers;
using Microsoft.AspNetCore.Mvc;
using BackEndTestTask.Models.Api;
using BackEndTestTask.Services.Interfaces;

namespace BackEndTestTask.Controllers
{
    public class JournalController : ControllerBase
    {
        private readonly IExceptionJournalService _exceptionJournalService;

        public JournalController(IExceptionJournalService exceptionJournalService)
        {
            _exceptionJournalService = exceptionJournalService;
        }

        [HttpPost(ApiHelper.JournalGetRange)]
        public async Task<IActionResult> GetRange([FromBody] GetRangeEndpointData endpointData)
        {
            if (endpointData.Page <= 0 || endpointData.PageSize <= 0)
            {
                throw new SecureException(ErrorHelper.incorrectInputParameters);
            }

            var result = await _exceptionJournalService.GetRangeAsync(endpointData.Page, endpointData.PageSize);
            return result.IsSuccessful ? Ok(result.Data)
                                       : StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpPost(ApiHelper.JournalGetSingle)]
        public async Task<IActionResult> GetSingle([FromBody] GetSingleEndpointData endpointData)
        {
            if (string.IsNullOrWhiteSpace(endpointData.EventId))
            {
                throw new SecureException(ErrorHelper.incorrectInputParameters);
            }

            var result = await _exceptionJournalService.GetSingleAsync(endpointData.EventId);
            return result.IsSuccessful ? Ok(result.Data)
                                       : StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}
