using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


// This script includes all the functions needed for navigating the UI.

public class UI_Navigation : MonoBehaviour
{
    // Play Button
    public void PlayGame()
    {
        StartCoroutine(DelayedPlay());
    }

    private IEnumerator DelayedPlay()
    {
        Debug.Log("Pressed Play Button");
        yield return new WaitForSeconds(2f); // Warte 2 Sekunden
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    // Restart Button
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Restartet Scene");
    }


    // Quit Button
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitted Game");
    }


    // Back to Menu Button
    public void BackToMenu()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Switched back to Main Menu");
    }
}
