using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private int level;

    private WaveManager _waveManager;
    private DataManager _dataManager;
    private LevelManager _levelManager;

    private void Start()
    {
        winScreen.SetActive(false);

        _waveManager = GetComponent<WaveManager>();
        _dataManager = GetComponent<DataManager>();
        _levelManager = FindFirstObjectByType<LevelManager>();
    }

    private void Update()
    {
        if (_waveManager.hasSpawnedEverything && !_dataManager.IsDead)
        {
            _levelManager.AddLevel(level);
            winScreen.SetActive(true);
        }
    }
}
