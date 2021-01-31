using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    Canvas gameOver;
    [SerializeField] float loadDelay = 1f;

    [SerializeField]
    private TMPro.TextMeshProUGUI scoreText;

    float timePassed = 0f;

    private void Awake()
    {
        gameOver = GetComponentInChildren<Canvas>();
        gameOver.enabled = false;

        GameManager.Instance.onGameLost += OnGameLost;
    }

    private void OnDestroy()
    {
        GameManager.Instance.onGameLost -= OnGameLost;
    }

    private void OnGameLost()
    {
        StartGameOverMenuCoroutine();
    }

    public void StartGameOverMenuCoroutine()
    {
        StartCoroutine(LoadGameOverMenu());
    }


    public IEnumerator LoadGameOverMenu()
    {
        scoreText.text = "SCORE : " + ScoreKeeper.Instance.score;
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
        SceneManager.LoadScene("Main Menu");
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
