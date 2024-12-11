using TMPro;
using UnityEngine;

public class ScoreViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void UpdateScore(int score) => 
        _text.text = "Очки: " + score;
}
