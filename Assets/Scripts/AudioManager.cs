using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip backgroundMusic;
    public AudioClip eatSoundEffect;
    public AudioClip gameOverSoundEffect;
    public AudioClip buttonClickSoundEffect;

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
    public void InitializeBackgroundMusic()
    {
        int isMusic = PlayerPrefs.GetInt("isMusic", 1);
        if(isMusic == 1)
        {
            audioSource.clip = backgroundMusic;
            audioSource.Play();
        }
        else if (isMusic == 0)
        {
            audioSource.Stop();
        }
    }
    public void PlayEatSoundEffect()
    {
        audioSource.PlayOneShot(eatSoundEffect);
    }
    public void PlayGameOverSoundEffect()
    {
        audioSource.PlayOneShot(gameOverSoundEffect);
    }
    public void PlayButtonClickSoundEffect()
    {
        audioSource.PlayOneShot(buttonClickSoundEffect);
    }
}
