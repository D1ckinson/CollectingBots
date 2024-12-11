using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Resource : MonoBehaviour, IPoolableObject<Resource>
{
    private Rigidbody _rigidbody;

    public event Action<Resource> IDisable;

    private void Awake() =>
        _rigidbody = GetComponent<Rigidbody>();

    public void Enable()
    {
        gameObject.SetActive(true);
        _rigidbody.isKinematic = true;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
        IDisable?.Invoke(this);
    }
}