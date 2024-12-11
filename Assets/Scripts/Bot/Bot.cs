using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Collector))]
public class Bot : MonoBehaviour
{
    [SerializeField] private Base _base;

    private Mover _mover;
    private Collector _collector;

    public bool IsBusy {  get; private set; }

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _collector = GetComponent<Collector>();
    }

    public void ExtractResource(Resource resource)
    {
        _mover.SetTarget(resource.transform);
        _mover.TargetReached += _collector.PickUp;
        _collector.ItemPicked += ReturnToBase;
        IsBusy = true;
    }

    private void ReturnToBase()
    {
        _mover.SetTarget(_base.transform);
        _mover.TargetReached -= _collector.PickUp;
        _mover.TargetReached += GiveResource;
    }

    private void GiveResource(Transform _)
    {
        _base.GetResource(_collector.Relieve().GetComponent<Resource>());
        _mover.TargetReached -= GiveResource;
        IsBusy = false;
    }
}
