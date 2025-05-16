namespace TaskManagement.Domains
{
    public abstract class Entity<T> where T : IEquatable<T>
    {
        public T Id { get; protected set; }   
    }
}
