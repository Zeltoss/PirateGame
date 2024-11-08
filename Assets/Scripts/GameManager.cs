using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    private PlayerControls _playerControls;
    private InputAction pauseAction;

    public delegate void OnPausingGame();
    public static OnPausingGame onPausingGame;
    public static OnPausingGame onResumingGame;

    private bool isPaused;


    void Awake()
    {
        _playerControls = new PlayerControls();
        ResumeGame();
    }


    private void OnEnable()
    {
        pauseAction = _playerControls.UI.Pause;
        pauseAction.Enable();
        pauseAction.performed += PressedPause;
    }

    private void OnDisable()
    {
        pauseAction.Disable();
    }



    private void PressedPause(InputAction.CallbackContext callbackContext)
    {
        if (!isPaused)
        {
            PauseGame();
            onPausingGame?.Invoke();
        }
        else
        {
            ResumeGame();
            onResumingGame?.Invoke();
        }
    }



    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
    }


    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
    }
}
