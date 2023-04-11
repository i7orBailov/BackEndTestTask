using BackEndTestTask.Models.Database.Interfaces;
using Newtonsoft.Json;

namespace BackEndTestTask.Models.Database
{
    public class Node : INode<string>
    {
        public Node() { }

        public Node(string name)
        {
            Name = name;
        }

        public Node(string name, int? parentId) : this(name)
        {
            ParentId = parentId;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public int? ParentId { get; set; }

        [JsonIgnore]
        public virtual Node Parent { get; set; }

        public virtual ICollection<Node> Children { get; set; }
    }
}