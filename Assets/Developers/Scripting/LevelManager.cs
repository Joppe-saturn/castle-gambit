using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<int> completedLevels = new List<int>();

    private LevelManager[] levelManagers;
    private bool original;

    private void Start()
    {
        levelManagers = Resources.FindObjectsOfTypeAll<LevelManager>();
        
        if (levelManagers.Length > 1 && !original)
        {
            Destroy(gameObject);
        }
        
        original = true;
        DontDestroyOnLoad(gameObject);
        completedLevels.Add(0);
    }

    public void AddLevel(int level)
    {
        bool newLevel = true;
        for(int i = 0; i < completedLevels.Count; i++)
        {
            if (completedLevels[i] == level)
            {
                newLevel = false;
                break;
            }
        }

        if (newLevel)
        {
            completedLevels.Add(level);
        }
    }
}
