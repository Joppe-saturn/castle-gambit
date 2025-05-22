using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager _instance;
    public static DataManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = FindFirstObjectByType<DataManager>();
        }
        return _instance;
    }

    [System.Serializable]
    public class Path
    {
        public Transform[] _path;
    }
    [SerializeField] private Path[] _pathArray;

    public Transform[] Paths(int i)
    {
        return _pathArray[i]._path;
    }

    public enum ShopState
    {
        Open,
        Closed,
        Placing
    }

    private ShopState _shopState;
    public ShopState State
    {
        get
        {
            return _shopState;
        }
        set
        {
            _shopState = value;
        }
    }

    private int _money;
    public int Money
    {
        get
        {
            return _money;
        }
        set
        {
            _money = value;
        }
    }

    private TowerPlacer.Tower _currentTower;
    public TowerPlacer.Tower CurrentTower
    {
        get
        {
            return _currentTower;
        }
        set
        {
            _currentTower = value;
        }
    }

    private bool _isPlacingWhite;
    public bool IsPlacingWhite
    {
        get
        {
            return _isPlacingWhite;
        }
        set
        {
            _isPlacingWhite = value;
        }
    }

    public List<GameObject> Rooks = new List<GameObject>();

}