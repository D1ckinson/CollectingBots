using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ScoreDisplay))]
public class Base : MonoBehaviour
{
    [SerializeField] private List<Bot> _bots;
    [SerializeField] private float _scanDelay = 1f;
    [SerializeField] private ResourceScanner _scanner;
    [SerializeField] private Transform _returnPoint;

    private ScoreDisplay _scoreDisplay;
    private ResourceCounter _resourceCounter = new();
    private List<Resource> _foundedResources = new();
    private List<Resource> _resourcesInProgress = new();

    private void Awake() =>
        _scoreDisplay = GetComponent<ScoreDisplay>();

    private void Start() =>
        StartCoroutine(Work());

    private void OnEnable() =>
        _resourceCounter.QuantityChanged += _scoreDisplay.UpdateText;

    private void OnDisable() =>
        _resourceCounter.QuantityChanged -= _scoreDisplay.UpdateText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource) == false)
            return;

        if (_resourcesInProgress.Contains(resource) == false)
            return;

        resource.transform.SetParent(null);
        resource.TurnOnRigidbody();
        resource.Collect();

        _resourcesInProgress.Remove(resource);
        _resourceCounter.PlusOne();
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
