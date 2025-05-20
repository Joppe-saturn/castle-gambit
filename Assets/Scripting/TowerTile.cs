using UnityEngine;

public class TowerTile : MonoBehaviour, IClickable
{
    private DataManager _dataManager;
    private TowerPlacer.Tower placedTower;

    private void Start()
    {
        _dataManager = DataManager.GetInstance();
    }

    public void OnClick()
    {
        if (_dataManager.CurrentTower != null)
        {
            if (placedTower == null)
            {
                if (_dataManager.CurrentTower.Price <= _dataManager.Money)
                {
                    _dataManager.Money -= _dataManager.CurrentTower.Price;
                    placedTower = _dataManager.CurrentTower;
                    placedTower.gameObject = Instantiate(_dataManager.CurrentTower.gameObject, transform.position, Quaternion.identity);
                    _dataManager.CurrentTower = null;
                }
            }
        }
    }
}
