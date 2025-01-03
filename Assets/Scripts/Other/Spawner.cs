using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Spawner<T> : MonoBehaviour where T : MonoBehaviour, IPoolableObject<T>
{
    [SerializeField] private T _item;
    [SerializeField] private float _delay = 2f;

    private BoxCollider _spawnArea;
    private Pool<T> _pool;

    private void Awake()
    {
        _spawnArea = GetComponent<BoxCollider>();

        _pool = new Pool<T>(() => Instantiate(_item));
    }

    private void Start() =>
        StartCoroutine(Spawn());

    private IEnumerator Spawn()
    {
        WaitForSeconds wait = new(_delay);

        while (true)
        {
            yield return wait;

            T item = _pool.Get();
            item.transform.position = GetSpawnPoint();
        }
    }

    private Vector3 GetSpawnPoint()
    {
        float halfDivider = 2;

        float halfX = _spawnArea.size.x / halfDivider;
        float halfZ = _spawnArea.size.z / halfDivider;

        Vector2 topRight = new(transform.position.x + halfX, transform.position.z + halfZ);
        Vector2 bottomLeft = new(transform.position.x - halfX, transform.position.x - halfZ);

        float x = Random.Range(topRight.x, bottomLeft.x);
        float z = Random.Range(topRight.y, bottomLeft.y);

        return new Vector3(x, transform.position.y, z);
    }
}
