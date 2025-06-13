using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private DataManager _dataManager;

    private void Start()
    {
        _dataManager = DataManager.GetInstance();
    }

    private void Update()
    {
        Debug.Log(_dataManager.Money);
    }
}
