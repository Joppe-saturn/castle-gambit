using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;

    private WaveManager _waveManager;
    private DataManager _dataManager;

    private void Start()
    {
        winScreen.SetActive(false);

        _waveManager = GetComponent<WaveManager>();
        _dataManager = GetComponent<DataManager>();
    }

    private void Update()
    {
        if (_waveManager.hasSpawnedEverything && !_dataManager.IsDead)
        {
            winScreen.SetActive(true);
        }
    }
}
