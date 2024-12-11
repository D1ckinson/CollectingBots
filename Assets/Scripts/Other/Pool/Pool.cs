using System;
using System.Collections.Generic;
using System.Linq;

public class Pool<T> where T : IPoolableObject<T>
{
    private Func<T> _createFunc;
    //private Action<T> _actionOnGet;
    //private Action<T> _actionOnReturn;
    private Queue<T> _items = new();

    public Pool(Func<T> createFunc/*, Action<T> actionOnGet = null, Action<T> actionOnReturn = null*/, int preloadCount = 1)
    {
        _createFunc = createFunc;
        //_actionOnGet = actionOnGet;
        //_actionOnReturn = actionOnReturn;

        for (int i = 0; i < preloadCount; i++)
        {
            T item = _createFunc.Invoke();
            item.Disable();
            _items.Enqueue(item);
        }
    }

    public T Get()
    {
        T item = _items.Any() ? _items.Dequeue() : _createFunc.Invoke();
        item.Enable();
        item.IDisable += Return;
        //_actionOnGet?.Invoke(item);

        return item;
    }

    public void Return(T item)
    {
        //item.Disable();
        //_actionOnReturn?.Invoke(item);
        item.IDisable -= Return;
        _items.Enqueue(item);
    }
}
