using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ResourceScanner))]
[RequireComponent(typeof(ScoreViewer))]
public class Base : MonoBehaviour, IHaveScore
{
    [SerializeField] private float _scanDelay = 1f;
    [SerializeField] private Bot _botPrefab;
    [SerializeField] private Transform _botPoint;
    [SerializeField] private int _botCost = 3;

    private ResourceScanner _resourceScanner;
    private List<Bot> _bots = new();
    private List<Resource> _resources;
    private List<Resource> _extractiveResources = new();
    private int _resourceCount = 0;
    private Action _spendResources;

    public event Action<int> ScoreChange;
    public event Action<Base> WasClicked;

    private void Awake()
    {
        _resourceScanner = GetComponent<ResourceScanner>();
        ScoreChange?.Invoke(_resourceCount);
        _spendResources = BuildBot;
    }

    //private void Start() =>
    //    StartCoroutine(ExtractResources());

    private void OnMouseUp() =>
        WasClicked?.Invoke(this);

    public void GetResource(Resource resource)
    {
        _extractiveResources.Remove(resource);
        resource.Disable();

        _resourceCount++;
        _spendResources?.Invoke();

        ScoreChange?.Invoke(_resourceCount);
    }

    private void BuildBot()
    {
        if (_resourceCount < _botCost)
            return;

        _resourceCount -= _botCost;
        InitBot();
    }

    public void InitBot()
    {
        Bot bot = Instantiate(_botPrefab, _botPoint.position, Quaternion.identity);

        bot.SetBase(this);
        _bots.Add(bot);
    }

    public void AmassResources(Action action)
    {
        _spendResources = action;
    }

    private IEnumerator ExtractResources()
    {
        WaitForSeconds wait = new(_scanDelay);

        _resourceScanner.Run();
        _resourceScanner.Scanned += GetScanResults;

        while (true)
        {
            for (int i = 0; i < _resources.Count; i++)
            {
                Bot bot = _bots.FirstOrDefault(bot => bot.IsBusy == false);

                if (bot == null)
                {
                    break;
                }

                Resource resource = _resources.First();
                _resources.RemoveAt(i);

                _extractiveResources.Add(resource);
                bot.ExtractResource(resource);
            }

            yield return wait;
        }
    }

    private void GetScanResults(IEnumerable<Resource> resources) =>
        _resources.AddRange(resources.Except(_extractiveResources));
}
