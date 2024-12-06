using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceScanner : MonoBehaviour 
{
    [SerializeField] private Collider _collider;

    public List<Resource> _foundedItems = new();

    public List<Resource> FoundedResources => _foundedItems.ToList();

    private void Start()
    {
        if (_collider == null)
            throw new ArgumentException("Зона сканирования не установлена");

        if (_collider.isTrigger == false)
            throw new ArgumentException("Зона сканирования должна быть триггером");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource) == false)
            return;

        _foundedItems.Add(resource);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Resource resource) == false)
            return;

        _foundedItems.Remove(resource);
    }
}
