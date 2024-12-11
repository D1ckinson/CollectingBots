using System;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    public event Action<Transform> TargetReached;

    private Transform _target;
    private NavMeshAgent _agent;
    private float _arrivalThreshold = 1f;

    public void SetTarget(Transform target)
    {
        _target = target;
        _agent.SetDestination(_target.position);
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _agent.SetDestination(new(0, 0, 0));
    }

    private void Update()
    {
        if (_agent.remainingDistance < _arrivalThreshold)
        {
            _agent.isStopped = true;
            TargetReached?.Invoke(_target);
        }
    }
}
