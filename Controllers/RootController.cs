using System.Net;
using BackEndTestTask.Models;
using BackEndTestTask.Helpers;
using Microsoft.AspNetCore.Mvc;
using BackEndTestTask.Models.Api;
using BackEndTestTask.Services.Interfaces;

namespace BackEndTestTask.Controllers
{
    public class RootController : ControllerBase
    {
        private readonly INodeService _nodeService;

        public RootController(INodeService nodeService)
        {
            _nodeService = nodeService;
        }

        [HttpPost(ApiHelper.RootGet)]
        public async Task<IActionResult> Get([FromBody] GetRootEndpointData endpointData)
        {
            if (string.IsNullOrWhiteSpace(endpointData.RootName))
            {
                throw new SecureException(ErrorHelper.incorrectInputParameters);
            }
            var result = await _nodeService.GetEntireTree(endpointData.RootName);
            return result.IsSuccessful ? Ok(result.Data) 
                                       : StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}
