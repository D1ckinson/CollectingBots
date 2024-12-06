using System;
using System.Linq;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private Transform _collectPoint;
    [SerializeField] private float _collectRadius = 5f;

    private void Start()
    {
        if (_collectPoint == null)
            throw new ArgumentException("Точка сбора не установлена");
    }

    public void Collect()
    {
        Resource resource = Physics.OverlapSphere(transform.position, _collectRadius)
            .Select(collider => collider.GetComponent<Resource>())
            .Where(resource => resource != null).FirstOrDefault();

        if (resource == null)
            return;

        resource.transform.SetParent(_collectPoint);
        resource.transform.position = _collectPoint.position;
        resource.TurnOffRigidbody();
    }
}