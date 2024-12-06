using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Collector))]
public class Bot : MonoBehaviour
{
    private Vector3 _returnPoint;
    private Mover _mover;
    private Collector _collector;

    public bool IsBusy { get; private set; }

    private void Start()
    {
        _mover = GetComponent<Mover>();
        _collector = GetComponent<Collector>();
    }

    public void GoToResource(Vector3 resourcePosition,Vector3 returnPoint)
    {
        IsBusy = true;

        _returnPoint = returnPoint;
        _mover.SetTarget(resourcePosition);
        _mover.TargetReached += Collect;
    }

    private void Collect()
    {
        _mover.TargetReached -= Collect;
        _collector.Collect();

        Return();
    }

    private void Return()
    {
        _mover.SetTarget(_returnPoint);
        _mover.TargetReached += SetBusyToFalse;
    }

    private void SetBusyToFalse()
    {
        _mover.TargetReached -= SetBusyToFalse;
        IsBusy = false;
    }
}
