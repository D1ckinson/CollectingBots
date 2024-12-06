using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Counter))]
public class Base : MonoBehaviour
{
    [SerializeField] private List<Bot> _bots;
    [SerializeField] private float _scanDelay = 1f;
    [SerializeField] private ResourceScanner _scanner;
    [SerializeField] private Transform _returnPoint;

    public event Action<string> ScoreChanged;

    private Counter _counter;
    private List<Resource> _foundedResources = new();
    private List<Resource> _resourcesInProgress = new();
    private int _score = 0;

    private void OnEnable()
    {
        _counter = GetComponent<Counter>();
        ScoreChanged += _counter.UpdateText;
    }

    private void OnDisable()
    {
        ScoreChanged -= _counter.UpdateText;
    }

    private void Start()
    {
        StartCoroutine(Work());
        _counter.UpdateText(_score.ToString());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource) == false)
            return;

        resource.transform.SetParent(null);
        resource.TurnOnRigidbody();
        resource.Collect();

        _resourcesInProgress.Remove(resource);

        _score++;
        ScoreChanged?.Invoke(_score.ToString());
    }

    private IEnumerator Work()
    {
        WaitForSeconds wait = new(_scanDelay);

        while (true)
        {
            yield return wait;

            IEnumerable<Resource> resources = _scanner.FoundedResources.Except(_resourcesInProgress);

            if (resources.Any())
                _foundedResources.AddRange(resources);

            if (_foundedResources.Any() == false)
                continue;

            SendBots();
        }
    }

    private void SendBots()
    {
        List<Bot> freeBots = _bots.Where(bot => bot.IsBusy == false).ToList();

        for (int i = 0; i < freeBots.Count; i++)
        {
            if (_foundedResources.Any() == false)
                return;

            Bot bot = freeBots[i];
            Resource resource = _foundedResources.First();

            _foundedResources.Remove(resource);
            _resourcesInProgress.Add(resource);

            bot.GoToResource(resource.transform.position, _returnPoint.position);
        }
    }
}
