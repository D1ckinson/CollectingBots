using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Collector))]
public class Bot : MonoBehaviour
{
    private Base _base;
    private Mover _mover;
    private Collector _collector;
    private Resource _resource;

    public bool IsBusy { get; private set; }

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _collector = GetComponent<Collector>();
    }

    public void SetBase(Base @base) =>
        _base = @base;

    public void ExtractResource(Resource resource)
    {
        _resource = resource;

        _collector.ItemPicked += ReturnToBase;
        _mover.TargetReached += PickUp;

        _mover.SetTarget(_resource.transform.position);
        IsBusy = true;
    }

    private void PickUp() =>
        _collector.PickUp(_resource.transform);

    private void ReturnToBase()
    {
        _collector.ItemPicked -= ReturnToBase;
        _mover.TargetReached -= PickUp;
        _mover.TargetReached += GiveResource;

        _mover.SetTarget(_base.transform.position);
    }

    private void GiveResource()
    {
        _mover.TargetReached -= GiveResource;

        _base.GetResource(_collector.Relieve().GetComponent<Resource>());
        IsBusy = false;
    }
}
