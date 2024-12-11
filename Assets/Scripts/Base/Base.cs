using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ResourceScanner))]
[RequireComponent(typeof(ScoreViewer))]
public class Base : MonoBehaviour
{
    [SerializeField] private float _scanDelay = 1f;
    [SerializeField] private List<Bot> _bots;

    private ResourceScanner _resourceScanner;
    private ScoreViewer _scoreViewer;
    private List<Resource> _extractedResources = new();
    private int _score = 0;

    private void Awake()
    {
        _resourceScanner = GetComponent<ResourceScanner>();
        _scoreViewer = GetComponent<ScoreViewer>();
    }

    private void Start()
    {
        _scoreViewer.UpdateScore(_score);
        StartCoroutine(ExtractResources());
    }

    public void GetResource(Resource resource)
    {
        resource.Disable();
        _scoreViewer.UpdateScore(++_score);
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
