using System;
using TMPro;
using UnityEngine;

public class ScoreViewer<T> : MonoBehaviour where T : MonoBehaviour, IHaveScore
{
    [SerializeField] private TMP_Text _text;

    private T _scoreComponent;

    private void Awake()
    {
        if (TryGetComponent(out T scoreComponent) == false)
            new Exception("Object does not contain score holder");

        _scoreComponent = scoreComponent;
    }

    private void OnEnable() =>
        _scoreComponent.ScoreChange += UpdateScore;

    private void OnDisable() =>
        _scoreComponent.ScoreChange -= UpdateScore;

    private void UpdateScore(int score) =>
        _text.text = "Очки: " + score;
}
