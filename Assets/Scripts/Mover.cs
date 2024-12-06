using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _targetDistance = 1f;

    public event UnityAction TargetReached;

    private Rigidbody _rigidbody;
    private Vector3 _target;
    private bool _isTargetSet;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_isTargetSet == false)
            return;

        Move();
    }

    public void SetTarget(Vector3 target)
    {
        _isTargetSet = true;
        _target = target;
    }

    private void Move()
    {
        Vector3 direction = _target - transform.position;
        direction.y = 0f;

        float sqrDistance = Vector3.SqrMagnitude(direction);

        if (sqrDistance < _targetDistance)
        {
            _isTargetSet = false;
            TargetReached?.Invoke();
        }

        _rigidbody.velocity = direction.normalized * _speed;
    }
}
