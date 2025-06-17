using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject backGround;

    private int pauseState = 1;

    public void Pause()
    {
        pauseState = 1 - pauseState;
        Time.timeScale = pauseState;
        backGround.SetActive(pauseState == 0);
    }
}
