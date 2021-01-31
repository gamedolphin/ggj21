using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void OnGameWin(Vector3 position);
public delegate void OnGameLost();
public delegate void OnLoadNextLevel();
public delegate void OnGamePause(bool state);

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = new GameObject("Game Manager");
                instance = obj.AddComponent<GameManager>();
            }
            return instance;
        }
    }

    public event OnGameWin onGameWin;
    public event OnGameLost onGameLost;
    public event OnLoadNextLevel onLoadNextLevel;
    public event OnGamePause onGamePause;

    public float flashTime = 2.0f;

    public bool gameOver = false;
    public int minBoolCount = 0;

    public void GameWon(Vector3 pos)
    {
        onGameWin?.Invoke(pos);
        gameOver = true;
    }

    public void GameLost()
    {
        onGameLost?.Invoke();
        gameOver = true;
    }

    private int boidCount = 0;
    public void OnBoidCreated()
    {
        boidCount += 1;
    }

    public void SetBoolCount(int i)
    {
        minBoolCount += i;
    }

    public void OnBoidDestroyed(bool inverted, Vector3 pos)
    {
        boidCount -= 1;

        if (boidCount == minBoolCount)
        {
            if (inverted)
            {
                GameWon(pos);
            }
        }
    }

    public void LoadNextLevel()
    {
        Debug.Log("Load next level");
        onLoadNextLevel?.Invoke();
    }

    public bool gamePaused = false;

    private void Awake()
    {
        Time.timeScale = 1.0f;
        gameOver = false;
        boidCount = 0;
        minBoolCount = 0;
    }

    private void Start()
    {
        gameOver = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamePaused = !gamePaused;
            onGamePause(gamePaused);
        }
    }
}
