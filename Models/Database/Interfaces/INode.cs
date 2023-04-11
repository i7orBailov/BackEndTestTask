namespace BackEndTestTask.Models.Database.Interfaces
{
    public interface INode<T>
    {
        public int Id { get; }
        public T Name { get; }
    }
}