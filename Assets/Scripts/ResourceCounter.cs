using System;

public class ResourceCounter
{
    public event Action<int> QuantityChanged;

    private int _quantity = 0;

    public void PlusOne()
    {
        _quantity++;

        QuantityChanged?.Invoke(_quantity);
    }
}
