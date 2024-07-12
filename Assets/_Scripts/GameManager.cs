using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;  
    public GameObject nextLevelUI;
    public TextMeshProUGUI timerText;
    public float gameDuration = 20f;
    private float timer;
    private bool gamePaused = false;



    void Start()
    {
        Time.timeScale = 1f;
        if (timerText != null)
        {
            timer = gameDuration;
            
            ShowGameUI();
        }
    }


    void Update()
    {
        if (!gamePaused)
        {
            if (timerText != null)
            {
                timer -= Time.deltaTime;
                timerText.text = timer.ToString("F2") + "s";

                if (timer <= 0)
                {
                    PauseGame();
                    ShowNextLevelUI();
                }
            }
        }
    }
    void PauseGame()
    {
        gamePaused = true;
        Time.timeScale = 0f;
    }


    void ShowGameUI()
    {
        gameOverUI.SetActive(false);
        if (nextLevelUI != null) 
            nextLevelUI.SetActive(false);
    }

    void ShowNextLevelUI()
    {
        if (nextLevelUI != null)
            nextLevelUI.SetActive(true);
    }

    public void LoadNextLevel()
    {
        
        SceneManager.LoadScene("Level2"); 
    }

    public void OnPlayerFell()
    {
        // Mostrar UI de Game Over
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            PauseGame();
            
        }
    }

    public void RestartLevel()
    {
        // Reiniciar el nivel actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}