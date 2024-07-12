using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuUI;  // Referencia al UI del menú principal
    public GameObject creditsUI;   // Referencia al UI de los créditos

    void Start()
    {
        ShowMainMenu();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene"); // Asegúrate de que el nombre coincida con tu escena de juego
    }

    public void ShowCredits()
    {
        mainMenuUI.SetActive(false);
        creditsUI.SetActive(true);
    }

    public void ShowMainMenu()
    {
        mainMenuUI.SetActive(true);
        creditsUI.SetActive(false);
    }
}
