using System.Net;
using Newtonsoft.Json;
using BackEndTestTask.Helpers;
using Microsoft.AspNetCore.Mvc;
using BackEndTestTask.Models.Api;
using BackEndTestTask.Services.Interfaces;
using BackEndTestTask.Models;

namespace BackEndTestTask.Controllers
{
    public class JournalController : ControllerBase
    {
        private readonly IExceptionJournalService _exceptionJournalService;

        public JournalController(IExceptionJournalService exceptionJournalService)
        {
            _exceptionJournalService = exceptionJournalService;
        }

        [HttpPost]
        [Route(ApiHelper.JournalGetRange)]
        public async Task<IActionResult> GetRange([FromBody] GetRangeEndpointData endpointData)
        {
            if (endpointData.Page <= 0 || endpointData.PageSize <= 0)
            {
                throw new SecureException(ErrorHelper.incorrectInputParameters);
            }

            var result = await _exceptionJournalService.GetRangeAsync(endpointData.Page, endpointData.PageSize);
            if (result.IsSuccessful)
            {
                var resultJson = JsonConvert.SerializeObject(result.Data);
                return Ok(resultJson);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route(ApiHelper.JournalGetSingle)]
        public async Task<IActionResult> GetSingle([FromBody] GetSingleEndpointData endpointData)
        {
            if (string.IsNullOrWhiteSpace(endpointData.EventId))
            {
                throw new SecureException(ErrorHelper.incorrectInputParameters);
            }

            var result = await _exceptionJournalService.GetSingleAsync(endpointData.EventId);
            if (result.IsSuccessful)
            {
                var resultJson = JsonConvert.SerializeObject(result.Data);
                return Ok(resultJson);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
