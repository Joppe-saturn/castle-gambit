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

    private void Update()
    {
        if (_dataManager.CurrentTower != null) 
        { 
        
        Debug.Log(_dataManager.CurrentTower.Name + " hoi");
        }
        else
        {
            Debug.Log("Null");
        }
    }

    public void LoadTower(int tower)
    {
        _dataManager.CurrentTower = towers[0];
    }

    public void ChangeColor(bool color)
    {
        _dataManager.IsPlacingWhite = color;
    }
}