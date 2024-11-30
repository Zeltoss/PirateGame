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
    [SerializeField] private GameObject winScreen;

    private bool gamePaused;



    private void OnEnable()
    {
        GameManager.onPausingGame += ActivatePauseScreen;
        GameManager.onResumingGame += DeactivatePauseScreen;
        GameManager.onGameOver += ShowGameOverScreen;
        GameManager.onWinningGame += ShowWinScreen;
    }


    private void OnDisable()
    {
        GameManager.onPausingGame -= ActivatePauseScreen;
        GameManager.onResumingGame -= DeactivatePauseScreen;
        GameManager.onGameOver -= ShowGameOverScreen;
        GameManager.onWinningGame -= ShowWinScreen;
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



    private void ShowWinScreen()
    {
        winScreen.SetActive(true);
    }

}
