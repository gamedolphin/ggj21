using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    Canvas pauseMenu;

    private void Start()
    {
        pauseMenu = GetComponentInChildren<Canvas>();
        pauseMenu.enabled = false;

        GameManager.Instance.onGamePause += TogglePause;
    }

    private void TogglePause(bool pause)
    {
        if (pause)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    private void Pause()
    {
        pauseMenu.enabled = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.enabled = false;
        Time.timeScale = 1f;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;
    }

    public bool IsGamePaused()
    {
        return pauseMenu.enabled;
    }

}
