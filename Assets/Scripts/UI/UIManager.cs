using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject howToPlay;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOver;

    private bool gamePaused;



    private void OnEnable()
    {
        GameManager.onPausingGame += ActivatePauseScreen;
        GameManager.onResumingGame += DeactivatePauseScreen;
        GameManager.onGameOver += ShowGameOverScreen;
    }


    private void OnDisable()
    {
        GameManager.onPausingGame -= ActivatePauseScreen;
        GameManager.onResumingGame -= DeactivatePauseScreen;
    }



    private void ActivatePauseScreen()
    {
        pauseMenu.SetActive(true);
    }


    private void DeactivatePauseScreen()
    {
        pauseMenu.SetActive(false);
    }



    private void ShowGameOverScreen()
    {
        gameOver.SetActive(true);
    }

}
