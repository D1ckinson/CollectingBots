using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private string _text = "Количество ресурсов: ";
    [SerializeField] private TMP_Text _uiText;

    private void Awake()
    {
        UpdateText(0);
    }

    public void UpdateText(int score)
    {
        _uiText.text = _text + score;
    }
}
