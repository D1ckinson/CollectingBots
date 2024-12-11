using System;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private Transform _point;

    private Transform _item;

    public event Action ItemPicked;

    public void PickUp(Transform item)
    {
        _item = item;
        _item.SetParent(_point);
        _item.localPosition = Vector3.zero;

        ItemPicked?.Invoke();
    }

    public Transform Relieve()
    {
        Transform item = _item;

        _item.SetParent(null);
        _item = null;

        return item;
    }
}
