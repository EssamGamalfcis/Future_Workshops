namespace TaskManagement.Domains
{
    public abstract class AggregateRoot<T> where T : IEquatable<T>
    {
        public T Id { get; protected set; }
        protected AggregateRoot() { }
    }
}
