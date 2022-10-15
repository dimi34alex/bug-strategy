using System;

public interface IPoolable<TElement>
{
    public event Action<TElement> ElementReturnEvent;
    public event Action<TElement> ElementDestroyEvent;
}

public interface IPoolable<TElement, TID> : IPoolable<TElement>
{
    public TID Identifier { get; }
}
