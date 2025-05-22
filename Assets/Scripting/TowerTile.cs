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
                    GameObject currentPiece = new();
                    if (_dataManager.IsPlacingWhite)
                    {
                        currentPiece = _dataManager.CurrentTower.whiteObjetc;
                    }
                    placedTower.whiteObjetc = Instantiate(currentPiece, transform.position, Quaternion.identity);
                    _dataManager.CurrentTower = null; 
                    _dataManager.State = DataManager.ShopState.Closed;
                }
            }
        }
    }
}
