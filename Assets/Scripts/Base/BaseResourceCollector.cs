using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseResourceCollector : MonoBehaviour, IHaveScore
{
    [SerializeField] private float _scanDelay = 1f;
    [SerializeField] private Bot _botPrefab;
    [SerializeField] private Transform _botPoint;
    [SerializeField] private int _botCost = 3;

    private ResourceScanner _resourceScanner;
    private List<Bot> _bots = new();
    private List<Resource> _extractedResources = new();
    private int _resourceCount = 0;

    public event Action<int> ScoreChange;
    public event Action<Base> WasClicked;

    private void Awake()
    {
        _resourceScanner = GetComponent<ResourceScanner>();
        ScoreChange?.Invoke(_resourceCount);
    }

    //private void Start() =>
    //    StartCoroutine(ExtractResources());

    //protected void StartExtractResources()=>
    //    StartCoroutine(ExtractResources());

    //private IEnumerator ExtractResources()
    //{
    //    WaitForSeconds wait = new(_scanDelay);

    //    while (true)
    //    {
    //        List<Resource> resources = _resourceScanner.Scan().Except(_extractedResources).ToList();

    //        for (int i = 0; i < resources.Count; i++)
    //        {
    //            Bot bot = _bots.FirstOrDefault(bot => bot.IsBusy == false);

    //            if (bot == null)
    //            {
    //                break;
    //            }

    //            Resource resource = resources[i];

    //            _extractedResources.Add(resource);
    //            bot.ExtractResource(resource);
    //        }

    //        yield return wait;
    //    }
    //}
}
