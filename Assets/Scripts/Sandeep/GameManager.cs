using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void OnGameWin(Vector3 position);
public delegate void OnGameLost();

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

    public void GameWon(Vector3 pos)
    {
        onGameWin?.Invoke(pos);
    }

    public void GameLost()
    {
        onGameLost?.Invoke();
    }

    private int boidCount = 0;
    public void OnBoidCreated()
    {
        boidCount += 1;
    }

    public void OnBoidDestroyed(bool inverted, Vector3 pos)
    {
        boidCount -= 1;

        if (boidCount == 1)
        {
            if (inverted)
            {
                GameWon(pos);
            }
        }
    }
}
