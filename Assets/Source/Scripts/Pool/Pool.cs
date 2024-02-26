using System;
using System.Collections.Generic;

public class Pool<TElement> where TElement : IPoolable<TElement>
{
    private readonly bool _expandable;
    private readonly Queue<TElement> _freeElements;
    private readonly LinkedList<TElement> _extractedElements;
    private readonly Func<TElement> _elementInstantiateDelegate;

    public Pool(Func<TElement> elementInstantiateDelegate, int capacity = 0, bool expandable = true)
    {
        if (capacity is 0 && !expandable)
            throw new ArgumentException("Non-expandable pool cannot have a capacity of 0");

        _expandable = expandable;
        _freeElements = new Queue<TElement>(capacity);
        _elementInstantiateDelegate = elementInstantiateDelegate;

        if(!expandable)
            _extractedElements = new LinkedList<TElement>();

        for (int i = 0; i < capacity; i++)
            InstantiateElement();
    }

    public TElement ExtractElement()
    {
        TElement element;

        if (_freeElements.Count is 0)
        {
            if (_expandable)
            {
                InstantiateElement();
            }
            else
            {
                element = _extractedElements.First.Value;
                _extractedElements.RemoveFirst();
                _extractedElements.AddLast(element);

                return element;
            }
        }

        element = _freeElements.Dequeue();
        element.ElementReturnEvent += ReturnElement;
        element.ElementDestroyEvent += ElementDestroy;

        if(!_expandable)
            _extractedElements.AddLast(element);

        TryCallElementExtractEvent(element);

        return element;
    }

    private void InstantiateElement()
    {
        TElement element = _elementInstantiateDelegate();
        _freeElements.Enqueue(element);
        TryCallElementReturnEvent(element);
    }

    private void TryCallElementReturnEvent(TElement element) => (element as IPoolEventListener)?.OnElementReturn();
    private void TryCallElementExtractEvent(TElement element) => (element as IPoolEventListener)?.OnElementExtract();

    private void ReturnElement(TElement element)
    {
        element.ElementReturnEvent -= ReturnElement;
        element.ElementDestroyEvent -= ElementDestroy;

        if (!_expandable)
            _extractedElements.Remove(element);

        _freeElements.Enqueue(element);
        TryCallElementReturnEvent(element);
    }

    private void ElementDestroy(TElement element)
    {
        element.ElementReturnEvent -= ReturnElement;
        element.ElementDestroyEvent -= ElementDestroy;

        if (!_expandable)
            _extractedElements.Remove(element);
    }
}


public class Pool<TElement, TID> where TElement : IPoolable<TElement, TID>
{
    private readonly bool _expandable;
    private readonly Dictionary<TID, Queue<TElement>> _freeElements;
    private readonly Dictionary<TID, LinkedList<TElement>> _extractedElements;
    private readonly Func<TID, TElement> _elementInstantiateDelegate;

    public Pool(Func<TID, TElement> elementInstantiateDelegate, IReadOnlyCollection<(TID, int)> startCapacity = null, bool expandable = true)
    {
        _expandable = expandable;
        _freeElements = new Dictionary<TID, Queue<TElement>>();
        _elementInstantiateDelegate = elementInstantiateDelegate;
        _extractedElements = new Dictionary<TID, LinkedList<TElement>>();

        if(startCapacity != null)
            foreach ((TID, int) id in startCapacity)
                for (int i = 0; i < id.Item2; i++)
                    InstantiateElement(id.Item1);

        foreach (var elements in _extractedElements)
            foreach (var element in elements.Value)
                TryCallElementExtractEvent(element);
    }

    public TElement ExtractElement(TID id)
    {
        TElement element;

        if (!_freeElements.ContainsKey(id))
            _freeElements.Add(id, new Queue<TElement>());

        if (_freeElements[id].Count is 0)
        {
            if (_expandable)
            {
                InstantiateElement(id);
            }
            else
            {
                element = _extractedElements[id].First.Value;
                _extractedElements[id].RemoveFirst();
                _extractedElements[id].AddLast(element);

                return element;
            }
        }

        element = _freeElements[id].Dequeue();
        element.ElementReturnEvent += ReturnElement;
        element.ElementDestroyEvent += ElementDestroy;

        if (!_expandable)
            _extractedElements[id].AddLast(element);

        TryCallElementExtractEvent(element);

        return element;
    }

    private void InstantiateElement(TID id)
    {
        TElement element = _elementInstantiateDelegate(id);

        if (!_freeElements.ContainsKey(id))
            _freeElements.Add(id, new Queue<TElement>());

        _freeElements[id].Enqueue(element);
    }

    private void TryCallElementReturnEvent(TElement element) => (element as IPoolEventListener)?.OnElementReturn();
    private void TryCallElementExtractEvent(TElement element) => (element as IPoolEventListener)?.OnElementExtract();

    private void ReturnElement(TElement element)
    {
        element.ElementReturnEvent -= ReturnElement;
        element.ElementDestroyEvent -= ElementDestroy;

        if (!_expandable)
            _extractedElements.Remove(element.Identifier);

        _freeElements[element.Identifier].Enqueue(element);
        TryCallElementReturnEvent(element);
    }

    private void ElementDestroy(TElement element)
    {
        element.ElementReturnEvent -= ReturnElement;
        element.ElementDestroyEvent -= ElementDestroy;

        if (!_expandable)
            _extractedElements[element.Identifier].Remove(element);
    }
}
