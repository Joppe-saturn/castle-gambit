using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    [System.Serializable]
    public class Tower
    {
        public string Name;
        public GameObject gameObject;
        public Mesh PrelookMesh;
        public int Price;
    }

    [SerializeField] private Tower[] towers;

    private DataManager _dataManager;


    private void Start()
    {
        _dataManager = DataManager.GetInstance();
    }

    public void LoadTower(int tower)
    {
        _dataManager.CurrentTower = towers[tower];
    }
}