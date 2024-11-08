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
        GameManager.onPausingGame += TogglePauseScreen;
        GameManager.onResumingGame += TogglePauseScreen;
        GameManager.onGameOver += ShowGameOverScreen;
    }


    private void OnDisable()
    {
        GameManager.onPausingGame -= TogglePauseScreen;
        GameManager.onResumingGame -= TogglePauseScreen;
    }



    private void TogglePauseScreen()
    {
        gamePaused = !gamePaused;
        pauseMenu.SetActive(gamePaused);
    }



    private void ShowGameOverScreen()
    {
        gameOver.SetActive(true);
    }

}
