using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject[] shopOpenObjects;
    [SerializeField] private GameObject[] shopCloseObjects;
    [SerializeField] private GameObject[] shopPlaceObjects;

    private DataManager _dataManager;

    private void Start()
    {
        _dataManager = DataManager.GetInstance();
        _dataManager.State = DataManager.ShopState.Closed;
    }

    public void OpenShop()
    {
        switch (_dataManager.State)
        {
            case DataManager.ShopState.Closed:
                ActivateArray(shopOpenObjects);
                DisableArray(shopCloseObjects);
                break;
            case DataManager.ShopState.Placing:
                ActivateArray(shopOpenObjects);
                DisableArray(shopPlaceObjects);
                break;
        }
        _dataManager.State = DataManager.ShopState.Open;
    }

    public void CloseShop()
    {
        switch (_dataManager.State)
        {
            case DataManager.ShopState.Open:
                ActivateArray(shopCloseObjects);
                DisableArray(shopOpenObjects);
                break;
            case DataManager.ShopState.Placing:
                ActivateArray(shopCloseObjects);
                DisableArray(shopPlaceObjects);
                break;
        }
        _dataManager.State = DataManager.ShopState.Closed;
    }

    public void PlaceShop()
    {
        switch (_dataManager.State)
        {
            case DataManager.ShopState.Closed:
                ActivateArray(shopPlaceObjects);
                DisableArray(shopCloseObjects);
                break;
            case DataManager.ShopState.Open:
                ActivateArray(shopPlaceObjects);
                DisableArray(shopOpenObjects);
                break;
        }
        _dataManager.State = DataManager.ShopState.Placing;
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
