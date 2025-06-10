using System.Collections;
using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    [System.Serializable]
    public class Tower
    {
        public string Name;
        public GameObject whiteObject;
        public GameObject blackObject;
        public Mesh PrelookMesh;
        public int Price;
    }

    [SerializeField] private Tower[] towers;

    [SerializeField] private DataManager _dataManager;

    private void Start()
    {
        _dataManager = DataManager.GetInstance();
        _dataManager.IsPlacingWhite = true;
    }

    public void LoadTower(int tower)
    {
        _dataManager.ClickOnButton = true;

        Tower currentTower = towers[tower];
        if (_dataManager.CurrentTower != currentTower)
        {
            _dataManager.CurrentTower = currentTower;
        }
        else
        {
            _dataManager.CurrentTower = null;
        }
    }

    public void ChangeColor(bool color)
    {
        _dataManager.IsPlacingWhite = color;
    }
}