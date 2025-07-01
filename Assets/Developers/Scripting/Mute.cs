using UnityEngine;
using System.Collections.Generic;

public class Mute : MonoBehaviour
{
    [SerializeField] private GameObject muteButton;
    [SerializeField] private GameObject unMuteButton;
    private AudioSource[] audioSources;
    private float defaultVolume;

    private void Start()
    {
        audioSources = GetComponents<AudioSource>();
        defaultVolume = audioSources[0].volume;
    }

    public void MuteMusic()
    {
        muteButton.SetActive(false);
        unMuteButton.SetActive(true);
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].volume = 0f;
        }
    }

    public void UnMuteMusic()
    {
        muteButton.SetActive(true);
        unMuteButton.SetActive(false);
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].volume = defaultVolume;
        }
    }
}
