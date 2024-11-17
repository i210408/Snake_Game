using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainMenuUI : MonoBehaviour
{   
    public GameObject mainMenu;
    public GameObject levelSelect;
    public GameObject settings;
    public TextMeshProUGUI level1Score;
    public TextMeshProUGUI level2Score;
    public Slider snakeSpeedSlider;
    public Slider volumeSlider;
    public AudioManager audioManagerReference;

    private void Start()
    {
        EnableMainMenu();
        DisableLevelSelect();
        DisableSettings();
        LoadLevelScores();
        float snakeSpeed = PlayerPrefs.GetFloat("snakeSpeed", 0.1f);
        snakeSpeedSlider.value = snakeSpeed;
        float volume = PlayerPrefs.GetFloat("volume", 0.5f);
        volumeSlider.value = volume;
    }

    private void Update()
    {

    }

    public void EnableMainMenu()
    {
        mainMenu.SetActive(true);
    }

    public void DisableMainMenu()
    {
        mainMenu.SetActive(false);
    }

    public void EnableLevelSelect()
    {
        levelSelect.SetActive(true);
    }

    public void DisableLevelSelect()
    {
        levelSelect.SetActive(false);
    }

    public void EnableSettings()
    {
        settings.SetActive(true);
    }

    public void DisableSettings()
    {
        settings.SetActive(false);
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene("Level" + level);
    }

    private void LoadLevelScores()
    {
        int level1 = PlayerPrefs.GetInt("highScore1", 0);
        int level2 = PlayerPrefs.GetInt("highScore2", 0);

        level1Score.text = level1.ToString();
        level2Score.text = level2.ToString();
    }

    public void ResetHighScore(int level)
    {
        PlayerPrefs.SetInt("highScore" + level.ToString(), 0);
        LoadLevelScores();
    }

    public void UpdateSnakeSpeed()
    {
        PlayerPrefs.SetFloat("snakeSpeed", snakeSpeedSlider.value);
    }

    public void UpdateVolume()
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
        audioManagerReference.UpdateVolume(volumeSlider.value);
    }

    public void ToggleMusic()
    {
        int isMusic =  PlayerPrefs.GetInt("isMusic", 1);
        if(isMusic == 1)
        {
            PlayerPrefs.SetInt("isMusic", 0);
            audioManagerReference.InitializeBackgroundMusic();
        }
        else if (isMusic == 0)
        {
            PlayerPrefs.SetInt("isMusic", 1);
            audioManagerReference.InitializeBackgroundMusic();
        }
    }
}
