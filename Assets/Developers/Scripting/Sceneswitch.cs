using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceneswitch : MonoBehaviour
{
    private LevelManager levelManager;

    private void Start()
    {
        levelManager = FindFirstObjectByType<LevelManager>();
    }

    public void SceneSwitchWithCheck(int scene)
    {
        bool canTransition = false;
        int prevLevel = scene - 2;

        for (int i = 0; i < levelManager.completedLevels.Count; i++)
        {
            if (levelManager.completedLevels[i] == prevLevel)
            {
                canTransition = true;
                break;
            }
        }

        if (canTransition)
        {
            SceneManager.LoadScene(scene);
        }
    }

    public void SceneSwitch(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
