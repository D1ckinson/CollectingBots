using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ResourceScanner))]
[RequireComponent(typeof(ScoreViewer<Base>))]
[RequireComponent(typeof(ScoreViewer<Base>))]
public class Base : MonoBehaviour, IHaveScore
{
    [SerializeField] private float _scanDelay = 1f;
    [SerializeField] private Bot _botPrefab;
    [SerializeField] private int _botsCount;
    [SerializeField] private Transform _botPoint;

    private ResourceScanner _resourceScanner;
    private List<Bot> _bots = new();
    private List<Resource> _extractedResources = new();
    private int _score = 0;

    public event Action<int> ScoreChange;

    private void Awake()
    {
        _resourceScanner = GetComponent<ResourceScanner>();
        ScoreChange?.Invoke(_score);

        for (int i = 0; i < _botsCount; i++)
        {
            Bot bot = Instantiate(_botPrefab, _botPoint.position, Quaternion.identity);
            bot.SetBase(this);
            _bots.Add(bot);
        }
    }

    private void Start() =>
        StartCoroutine(ExtractResources());

    public void GetResource(Resource resource)
    {
        _extractedResources.Remove(resource);
        ScoreChange?.Invoke(++_score);

        resource.Disable();
    }

    private IEnumerator ExtractResources()
    {
        WaitForSeconds wait = new(_scanDelay);

        while (true)
        {
            List<Resource> resources = _resourceScanner.Scan().Except(_extractedResources).ToList();

            for (int i = 0; i < resources.Count; i++)
            {
                Bot bot = _bots.FirstOrDefault(bot => bot.IsBusy == false);

                if (bot == null)
                {
                    break;
                }

                Resource resource = resources[i];

                _extractedResources.Add(resource);
                bot.ExtractResource(resource);
            }

            yield return wait;
        }
    }
}
