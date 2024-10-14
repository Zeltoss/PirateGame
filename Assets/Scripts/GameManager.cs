using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    private PlayerControls _playerControls;
    private InputAction pauseAction;

    private delegate void OnPausingGame();
    OnPausingGame onPausingGame;
    OnPausingGame onResumingGame;

    private bool isPaused;

    [SerializeField] private AudioClip waveSounds;



    void Awake()
    {
        _playerControls = new PlayerControls();
        SoundFXManager.instance.PlayLoopingSoundFXClip(waveSounds, transform, 1f);
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
        }
        else
        {
            ResumeGame();
        }
    }



    private void PauseGame()
    {
        isPaused = true;
        onPausingGame?.Invoke();
        Time.timeScale = 0;
    }


    private void ResumeGame()
    {
        isPaused = false;
        onResumingGame?.Invoke();
        Time.timeScale = 1;
    }
}
