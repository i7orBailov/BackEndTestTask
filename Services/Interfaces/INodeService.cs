using BackEndTestTask.Models.Database;
using BackEndTestTask.Models;

namespace BackEndTestTask.Services.Interfaces
{
    public interface INodeService
    {
        Task<ResponseMessage<List<Node>>> GetEntireTree(string name);
        Task<ResponseMessageBase> AddNodeToParentTree(int nodeParentId, string newNodeName);
        Task<ResponseMessageBase> RemoveNodeFromParent(int nodeId);
        Task<ResponseMessageBase> UpdateNode(int nodeId, string newNodeName);
    }
}