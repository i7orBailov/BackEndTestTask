using BackEndTestTask.Models;
using BackEndTestTask.Models.Database;
using BackEndTestTask.Services.Interfaces;
using BackEndTestTask.Models.Repositories.Interfaces;

namespace BackEndTestTask.Services.Business
{
    public class NodeService : INodeService
    {
        private readonly IBaseRepository<Node> _repository;

        public NodeService(IBaseRepository<Node> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets the entire tree by name.
        /// If the tree does not exist - is being created immediately
        /// </summary>
        /// <param name="name">Name of the root node</param>
        /// <returns>The whole tree structure in representation of nodes list</returns>
        public async Task<ResponseMessage<List<Node>>> GetEntireTree(string name)
        {
            var nodes = new List<Node>();
            var nodeElement = await _repository.GetFirstAsync(n => n.Name == name); // only one root node with the same name should exist
            nodeElement ??= await _repository.AddAsync(new Node(name));
            if (nodeElement is not null)
            {
                nodes.Add(nodeElement);
                AddChildrenByName(nodes, nodeElement);
            }
            return new ResponseMessage<List<Node>>(isSuccessful: true, data: nodes);
        }

        public async Task<ResponseMessageBase> AddNodeToParentTree(
            int nodeParentId, string newNodeName)
        {
            var nodeElement = await _repository.GetSingleAsync(n => n.Id == nodeParentId) 
                ?? throw new SecureException("Error: requested node does not exist.");

            if (nodeElement.Children.Any(n => n.Name == newNodeName))
            {
                throw new SecureException("Error: could not add because sibling with the same name already exists.");
            }

            var childNode = new Node(newNodeName, nodeParentId);
            nodeElement.Children.Add(childNode);
            await _repository.SaveChangesAsync();

            return new ResponseMessageBase(isSuccessful: true);
        }

        public async Task<ResponseMessageBase> RemoveNodeFromParent(int nodeId)
        {
            var nodeElement = await _repository.GetSingleAsync(n => n.Id == nodeId)
                ?? throw new SecureException("Error: requested node does not exist.");

            if (nodeElement.Children.Any())
            {
                throw new SecureException("Error: coud not delete as far as node has children.");
            }

            nodeElement.Parent?.Children.Remove(nodeElement);

            await _repository.DeleteAsync(nodeElement);
            return new ResponseMessageBase(isSuccessful: true);
        }

        public async Task<ResponseMessageBase> UpdateNode(int nodeId, string newNodeName)
        {
            var nodeElement = await _repository.GetFirstAsync(n => n.Id == nodeId)
                ?? throw new SecureException("Error: requested node does not exist.");

            if (nodeElement.Parent is not null &&
                nodeElement.Parent.Children.Any(n => n.Name == newNodeName))
            {
                throw new SecureException("Error: cannot update node name to a name that already exists among siblings.");
            }

            nodeElement.Name = newNodeName;
            await _repository.UpdateAsync(nodeElement);
            return new ResponseMessageBase(isSuccessful: true);
        }

        private void AddChildrenByName(List<Node> nodes, Node parent)
        {
            if (parent.Children is not null)
            {
                foreach (var child in parent.Children)
                {
                    if (child.Name == parent.Name)
                    {
                        nodes.Add(child);
                        AddChildrenByName(nodes, child);
                    }
                }
            }
        }
    }
}