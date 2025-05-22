using UnityEngine;

public class Rook : MonoBehaviour
{
    private DataManager _dataManager;

    private void Start()
    {
        _dataManager = DataManager.GetInstance();
        _dataManager.Rooks.Add(gameObject);
    }

    private void OnDestroy()
    {
        _dataManager.Rooks.Remove(gameObject);
    }
}
