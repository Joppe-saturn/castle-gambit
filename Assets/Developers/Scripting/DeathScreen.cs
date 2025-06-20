using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    private DataManager _dataManager;

    private bool isDead = false;

    [SerializeField] private GameObject deathScreen;
    [SerializeField] private float slowDownDeath;

    private void Start()
    {
        _dataManager = DataManager.GetInstance();   
    }

    private void Update()
    {
        if (_dataManager.IsDead && !isDead)
        {
            isDead = true;
            StartCoroutine(ShowDeathScreen());
        }
    }

    private IEnumerator ShowDeathScreen()
    {
        deathScreen.SetActive(true);

        while (Time.timeScale > 0.01f)
        {
            Time.timeScale /= slowDownDeath;
            yield return new WaitForSeconds(0.01f * Time.timeScale);
        }

        Time.timeScale = 0f;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
