using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private Resource _resource;
    [SerializeField] private float _spawnDelay=2f;

    private Pool<Resource> _pool;

    private Vector2 _topRight;
    private Vector2 _botLeft;
    private float _centerY;

    private void Start()
    {
        CalculateCorners();
        StartCoroutine(Spawn());

        _pool = new(() => Instantiate(_resource), 1);
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds wait = new(_spawnDelay);

        while (true)
        {
            yield return wait;

            Resource resource = _pool.Get();

            resource.gameObject.SetActive(true);
            resource.Collected += ReturnAction;
            resource.transform.position = GetCoordinates();
        }
    }

    private void ReturnAction(Resource resource)
    {
        resource.Collected -= ReturnAction;
        _pool.Return(resource);
    }

    private void CalculateCorners()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        float halfDivider = 2f;

        Vector2 flatCenter = new(collider.center.x, collider.center.z);
        Vector2 flatSize = new(collider.size.x, collider.size.z);

        _topRight.x = flatCenter.x + (flatSize.x / halfDivider);
        _topRight.y = flatCenter.y + (flatSize.y / halfDivider);

        _botLeft.x = flatCenter.x - (flatSize.x / halfDivider);
        _botLeft.y = flatCenter.y - (flatSize.y / halfDivider);

        _centerY = collider.center.y;
    }

    private Vector3 GetCoordinates()
    {
        float coordinateX = Random.Range(_topRight.x, _botLeft.x);
        float coordinateZ = Random.Range(_topRight.y, _botLeft.y);
        
        return new(coordinateX, _centerY, coordinateZ);
    }
}
