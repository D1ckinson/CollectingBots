using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private string _text = "���������� ��������: ";
    [SerializeField] private TMP_Text _uiText;

    public void UpdateText(string text)
    {
        _uiText.text = _text + text;
    }
}
