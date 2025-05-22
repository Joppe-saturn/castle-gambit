using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject[] shopOpenObjects;
    [SerializeField] private GameObject[] shopCloseObjects;
    [SerializeField] private GameObject[] shopPlaceObjects;

    private DataManager _dataManager;

    private DataManager.ShopState _currentState;

    private void Start()
    {
        _dataManager = DataManager.GetInstance();
        _dataManager.State = DataManager.ShopState.Closed;
        _currentState = DataManager.ShopState.Closed;
    }

    private void Update()
    {
        if (_currentState != _dataManager.State)
        {
            _currentState = _dataManager.State;
            switch (_dataManager.State)
            {
                case DataManager.ShopState.Closed:
                    CloseShop();
                    break;
                case DataManager.ShopState.Open:
                    OpenShop();
                    break;
                case DataManager.ShopState.Placing:
                    PlaceShop();
                    break;
            }
        }
    }

    public void OpenShop()
    {
        ActivateArray(shopOpenObjects);

        DisableArray(shopPlaceObjects);
        DisableArray(shopCloseObjects);

        _dataManager.State = DataManager.ShopState.Open;
        _currentState = _dataManager.State;
    }

    public void CloseShop()
    {
        ActivateArray(shopCloseObjects);

        DisableArray(shopOpenObjects);
        DisableArray(shopPlaceObjects);

        _dataManager.State = DataManager.ShopState.Closed;
        _currentState = _dataManager.State;
    }

    public void PlaceShop()
    {
        ActivateArray(shopPlaceObjects);

        DisableArray(shopCloseObjects);
        DisableArray(shopOpenObjects);

        _dataManager.State = DataManager.ShopState.Placing;
        _currentState = _dataManager.State;
    }

    private void ActivateArray(GameObject[] objects)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(true);
        }
    }

    private void DisableArray(GameObject[] objects)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(false);
        }
    }
}
