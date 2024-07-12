using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager2 : MonoBehaviour
{
    public GameObject gameOverUI;
    private bool gamePaused = false;







    void PauseGame()
    {
        gamePaused = true;
        Time.timeScale = 0f;
    }






    public void OnPlayerFell()
    {
        // Mostrar UI de Game Over
        if (gameOverUI != null)
        {
            //PauseGame();
            gameOverUI.SetActive(true);
        }
    }

    public void RestartLevel()
    {
        // Reiniciar el nivel actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}