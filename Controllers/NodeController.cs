using System.Net;
using BackEndTestTask.Helpers;
using Microsoft.AspNetCore.Mvc;
using BackEndTestTask.Models.Api;
using BackEndTestTask.Services.Interfaces;
using BackEndTestTask.Models;
using System.Reflection.Metadata;

namespace BackEndTestTask.Controllers
{
    [ApiController]
    public class NodeController : ControllerBase
    {
        private readonly INodeService _nodeService;

        public NodeController(INodeService nodeService)
        {
            _nodeService = nodeService;
        }

        [HttpPost]
        [Route(ApiHelper.NodeCreate)]
        public async Task<IActionResult> Create([FromBody] CreateNodeEndpointData endpointData)
        {
            if (string.IsNullOrWhiteSpace(endpointData.NewNodeName) || 
                string.IsNullOrWhiteSpace(endpointData.TreeParentName))
            {
                throw new SecureException(ErrorHelper.incorrectInputParameters);
            }
            else if (endpointData.ParentNodeId <= 0)
            {
                throw new SecureException(ErrorHelper.incorrectInputParameters);
            }

            var result = await _nodeService.AddNodeToParentTree(endpointData.ParentNodeId, endpointData.NewNodeName);
            if (result.IsSuccessful)
            {
                return Ok("successfully added node");
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route(ApiHelper.NodeRename)]
        public async Task<IActionResult> Rename([FromBody] UpdateNodeEndpointData endpointData)
        {
            if (string.IsNullOrWhiteSpace(endpointData.NewNodeName) ||
                string.IsNullOrWhiteSpace(endpointData.TreeParentName))
            {
                throw new SecureException(ErrorHelper.incorrectInputParameters);
            }
            else if (endpointData.NodeId <= 0)
            {
                throw new SecureException(ErrorHelper.incorrectInputParameters);
            }

            var result = await _nodeService.UpdateNode(endpointData.NodeId, endpointData.NewNodeName);
            if (result.IsSuccessful)
            {
                return Ok("successfully renamed node");
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route(ApiHelper.NodeDelete)]
        public async Task<IActionResult> Delete([FromBody] RemoveNodeEndpointData endpointData)
        {
            if (string.IsNullOrWhiteSpace(endpointData.TreeParentName))
            {
                throw new SecureException(ErrorHelper.incorrectInputParameters);
            }
            else if (endpointData.NodeId <= 0)
            {
                throw new SecureException(ErrorHelper.incorrectInputParameters);
            }

            var result = await _nodeService.RemoveNodeFromParent(endpointData.NodeId);
            if (result.IsSuccessful)
            {
                return Ok("successfully removed node");
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}