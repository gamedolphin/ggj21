using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void OnGameWin(Vector3 position);

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

    public void GameWon(Vector3 pos)
    {
        onGameWin?.Invoke(pos);
    }
}
