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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!IsGamePaused())
            {
                Pause();
            }
            else
            {
                Resume();
            }
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
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public bool IsGamePaused()
    {
        return pauseMenu.enabled;
    }

}
