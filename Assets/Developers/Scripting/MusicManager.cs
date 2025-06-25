using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private float musicLength = 124.578f;
    [SerializeField] private AudioSource firstMusicClip;
    [SerializeField] private AudioSource secondMusicSource;

    private void Start()
    {
        StartCoroutine(MusicLoop());
    }

    private IEnumerator MusicLoop()
    {
        while (true)
        {
            firstMusicClip.Play();
            yield return new WaitForSeconds(musicLength);
            secondMusicSource.Play();
            yield return new WaitForSeconds(musicLength);
        }
    }
}
