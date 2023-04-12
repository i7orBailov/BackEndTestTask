using System.Net;
using BackEndTestTask.Models;
using BackEndTestTask.Helpers;
using Microsoft.AspNetCore.Mvc;
using BackEndTestTask.Models.Api;
using BackEndTestTask.Services.Interfaces;

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

        [HttpPost(ApiHelper.NodeCreate)]
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
            return result.IsSuccessful ? Ok("Successfully added node")
                                       : StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpPost(ApiHelper.NodeRename)]
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
            return result.IsSuccessful ? Ok("Successfully renamed node")
                                       : StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpPost(ApiHelper.NodeDelete)]
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
            return result.IsSuccessful ? Ok("Successfully removed node")
                                       : StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}