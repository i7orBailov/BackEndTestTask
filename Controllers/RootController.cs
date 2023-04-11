using System.Net;
using Newtonsoft.Json;
using BackEndTestTask.Helpers;
using Microsoft.AspNetCore.Mvc;
using BackEndTestTask.Models.Api;
using BackEndTestTask.Services.Interfaces;
using BackEndTestTask.Models;

namespace BackEndTestTask.Controllers
{
    public class RootController : ControllerBase
    {
        private readonly INodeService _nodeService;

        public RootController(INodeService nodeService)
        {
            _nodeService = nodeService;
        }

        [HttpPost]
        [Route(ApiHelper.RootGet)]
        public async Task<IActionResult> Get([FromBody] GetRootEndpointData endpointData)
        {
            if (string.IsNullOrWhiteSpace(endpointData.RootName))
            {
                throw new SecureException(ErrorHelper.incorrectInputParameters);
            }

            var result = await _nodeService.GetEntireTree(endpointData.RootName);
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
