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
    public static OnPausingGame onGameOver;
    public static OnPausingGame onWinningGame;

    public AudioClip closeBookSound;

    private bool isPaused;

    [SerializeField] private GameObject skillBook;


    void Awake()
    {
        _playerControls = new PlayerControls();
        PauseGame();
    }


    private void OnEnable()
    {
        pauseAction = _playerControls.UI.Pause;
        pauseAction.Enable();
        pauseAction.performed += PressedPause;

        onGameOver += GameOver;
        onWinningGame += WonGame;
    }

    private void OnDisable()
    {
        pauseAction.Disable();

        onGameOver -= GameOver;
        onWinningGame -= WonGame;
    }



    private void PressedPause(InputAction.CallbackContext callbackContext)
    {
        if (skillBook.activeSelf)
        {
            skillBook.SetActive(false);
        }
        if (!isPaused)
        {
            PauseGame();
            onPausingGame?.Invoke();
        }
        else
        {
            ResumeGame();
            //onResumingGame?.Invoke();
            SoundFXManager.instance.PlaySoundFXClip(closeBookSound, transform, 1f);
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
        onResumingGame?.Invoke();
    }



    private void GameOver()
    {
        StartCoroutine(WaitForEndOfGame());
    }

    private void WonGame()
    {
        StartCoroutine(WaitForEndOfGame());
    }


    private IEnumerator WaitForEndOfGame()
    {
        yield return new WaitForSeconds(1);
        PauseGame();
    }

}
