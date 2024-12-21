using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Resource : MonoBehaviour, IPoolableObject<Resource>
{
    public event Action<Resource> Disabled;

    public void Enable() =>
        gameObject.SetActive(true);

    public void Disable()
    {
        gameObject.SetActive(false);
        Disabled?.Invoke(this);
    }
}