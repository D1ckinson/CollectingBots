using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Resource : MonoBehaviour
{
    public event Action<Resource> Collected;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Collect()
    {
        Collected?.Invoke(this);
    }

    public void TurnOffRigidbody()
    {
        _rigidbody.isKinematic = true;
    }

    public void TurnOnRigidbody()
    {
        _rigidbody.isKinematic = true;
    }
}
