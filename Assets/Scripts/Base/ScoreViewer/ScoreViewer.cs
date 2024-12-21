using TMPro;
using UnityEngine;

[RequireComponent(typeof(IHaveScore))]
public class ScoreViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private IHaveScore _scoreComponent;

    private void Awake() => 
        _scoreComponent = GetComponent<IHaveScore>();

    private void OnEnable() =>
        _scoreComponent.ScoreChange += UpdateScore;

    private void OnDisable() =>
        _scoreComponent.ScoreChange -= UpdateScore;

    private void UpdateScore(int score) =>
        _text.text = score.ToString();
}
