using System;
using UnityEngine;

public class Collector<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private Transform _point;

    private T _item;

    public event Action ItemPicked;

    public void PickUp(T item)
    {
        _item = item;
        _item.transform.SetParent(_point);
        _item.transform.localPosition = Vector3.zero;

        ItemPicked?.Invoke();
    }

    public T Relieve()
    {
        T item = _item;

        _item.transform.SetParent(null);
        _item = null;

        return item;
    }
}
