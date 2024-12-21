using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Scanner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private float _radius = 100f;
    [SerializeField] private float _scanDelay = 1f;

    private Coroutine _scanCoroutine;

    public event Action<IEnumerable<T>> Scanned;

    public void Run() =>
        _scanCoroutine = StartCoroutine(Scan());

    public void Stop()
    {
        if (_scanCoroutine != null)
            _scanCoroutine = null;
    }

    private IEnumerator Scan()
    {
        WaitForSeconds wait = new(_scanDelay);

        while (true)
        {
            yield return wait;

            IEnumerable<T> items = Physics.OverlapSphere(transform.position, _radius)
                .Select(collider => collider.GetComponent<T>())
                .Where(component => component != null);

            Scanned?.Invoke(items);
        }
    }
}
