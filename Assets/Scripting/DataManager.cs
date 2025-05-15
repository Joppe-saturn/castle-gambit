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
}
