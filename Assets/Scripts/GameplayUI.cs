using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    public TextMeshProUGUI scoreCounter;
    public TextMeshProUGUI highScoreCounter;
    public TextMeshProUGUI gameOverScore;
    public GameObject gameOverScreen;
    public GameObject pauseScreen;
    public Slider snakeSpeedSlider;
    public Slider volumeSlider;
    public Snake_Mechanics snakeMechanicsReference;
    public AudioManager audioManagerReference; 

    private void Start()
    {
        snakeSpeedSlider.value = snakeMechanicsReference.speed;
        volumeSlider.value = audioManagerReference.audioSource.volume;
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }
    private void Update()
    {
        snakeSpeedSlider.onValueChanged.AddListener(CallUpdateSnakeSpeed);
        volumeSlider.onValueChanged.AddListener(CallUpdateVolume);
    }
    public void DisplayScore(int score)
    {
        scoreCounter.text = score.ToString();
    }
    public void DisplayHighScore(int highScore)
    {
        highScoreCounter.text = highScore.ToString();
    }
    public void DisplayGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }
    public void DisplayPauseScreen()
    {
        pauseScreen.SetActive(true);
    }
    public void HidePauseScreen()
    {
        pauseScreen.SetActive(false);
    }
    public void DisplayGameOverScore(int score)
    {
        gameOverScore.text = score.ToString();
    }
    public void DisplayMainMenu()
    {
        Debug.Log("Main Menu Called");
    }
    public void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int index = currentScene.buildIndex;
        SceneManager.LoadScene(index);
        Time.timeScale = 1;
    }
    public void CallUpdateSnakeSpeed(float speed)
    {
        snakeMechanicsReference.UpdateSnakeSpeed(speed);
    }
    public void CallUpdateVolume(float volume)
    {
        audioManagerReference.UpdateVolume(volume);
    }
    
}
