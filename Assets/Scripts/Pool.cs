using System.Collections.Generic;
using System;
using UnityEngine;

public class Pool<T> where T : Component
{
    private Func<T> _preloadFunc;

    private Queue<T> _items = new();

    public Pool(Func<T> preloadFunc, int preloadCount)
    {
        _preloadFunc = preloadFunc;

        for (int i = 0; i < preloadCount; i++)
            Return(preloadFunc());
    }

    public T Get()
    {
        T item = _items.Count > 0 ? _items.Dequeue() : _preloadFunc();

        return item;
    }

    public void Return(T item)
    {
        item.gameObject.SetActive(false);
        _items.Enqueue(item);
    }
}