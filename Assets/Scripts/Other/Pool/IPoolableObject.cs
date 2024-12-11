using System;

public interface IPoolableObject<T>
{
    public event Action<T> IDisable;

    public void Enable();

    public void Disable();
}