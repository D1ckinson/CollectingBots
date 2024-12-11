using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Scanner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private float _radius = 100f;

    public IEnumerable<T> Scan()
    {
        return Physics.OverlapSphere(transform.position, _radius)
           .Select(collider => collider.GetComponent<T>())
           .Where(component => component != null);
    }
}
