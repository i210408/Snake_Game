using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip backgroundMusic;

    private void Awake()
    {
        float volume = PlayerPrefs.GetFloat("volume", 0.5f);
        UpdateVolume(volume);
    }
    private void Start()
    {
        
    }
    public void UpdateVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("volume", volume);
    }
}
