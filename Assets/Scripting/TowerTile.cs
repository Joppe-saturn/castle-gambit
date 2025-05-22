using UnityEngine;

public class TowerTile : MonoBehaviour, IClickable
{
    private DataManager _dataManager;
    private GameObject placedTower;

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
                    GameObject currentPiece = new();
                    if (_dataManager.IsPlacingWhite)
                    {
                        currentPiece = _dataManager.CurrentTower.whiteObject;
                    } 
                    else
                    {
                        currentPiece = _dataManager.CurrentTower.blackObject;
                    }
                    placedTower = Instantiate(currentPiece, transform.position, Quaternion.identity);
                    _dataManager.CurrentTower = null; 
                    _dataManager.State = DataManager.ShopState.Closed;
                }
            }
        }
    }
}
