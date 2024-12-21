using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 20f;

    private NavMeshAgent _agent;
    private float _arrivalThreshold = 1f;

    public event Action TargetReached;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        _agent.acceleration = float.MaxValue;
        _agent.speed = _speed;
    }

    private void Update()
    {
        if (_agent.remainingDistance < _arrivalThreshold)
            Stop();
    }

    public void SetTarget(Vector3 point)
    {
        _agent.SetDestination(point);
        _agent.isStopped = false;
    }

    private void Stop()
    {
        _agent.isStopped = true;
        TargetReached?.Invoke();
    }
}
