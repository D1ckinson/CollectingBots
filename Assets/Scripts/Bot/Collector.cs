using System;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private Transform _point;

    public event Action ItemPicked;

    private Transform _item;

    public void PickUp(Transform item)
    {
        _item = item;
        _item.SetParent(_point);
        _item.localPosition = Vector3.zero;
        ItemPicked?.Invoke();
    }

    public Transform Relieve()
    {
        _item.SetParent(null);

        Transform item = _item;
        _item = null;

        return item;
    }
}
