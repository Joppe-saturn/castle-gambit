using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    private DataManager _dataManager;
    private TextMeshProUGUI _textMeshProUGUI;

    private void Start()
    {
        _dataManager = DataManager.GetInstance();
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _textMeshProUGUI.text = "$" + _dataManager.Money.ToString();
    }
}
