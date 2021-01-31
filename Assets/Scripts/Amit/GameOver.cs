using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    Canvas gameOver;
    [SerializeField] float loadDelay = 1f;

    float timePassed = 0f;

    private void Start()
    {
        gameOver = GetComponentInChildren<Canvas>();
        gameOver.enabled = false;
    }

    public void StartGameOverMenuCoroutine()
    {
        StartCoroutine(LoadGameOverMenu());
    }


    public IEnumerator LoadGameOverMenu()
    {
        while (timePassed < loadDelay)
        {
            timePassed += Time.deltaTime;
            yield return null;
        }
        gameOver.enabled = true;
        Time.timeScale = 0f;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public bool IsGameOver()
    {
        return gameOver.enabled;
    }
}
