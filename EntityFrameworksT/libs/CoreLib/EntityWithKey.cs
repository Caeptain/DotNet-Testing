namespace CoreLib;

public abstract class EntityWithKey<TKey> : Entity
{
    public TKey Id { get; set; }
}