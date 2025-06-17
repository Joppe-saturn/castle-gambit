using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceneswitch : MonoBehaviour
{
    public void SceneSwitch(int Scene)
    {
        SceneManager.LoadScene(Scene);
    }
}
