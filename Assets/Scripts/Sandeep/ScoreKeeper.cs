using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private static ScoreKeeper instance;
    public static ScoreKeeper Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = new GameObject("Score Keeper");
                instance = obj.AddComponent<ScoreKeeper>();
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }

    public int score = 0;

    public System.Action<int> onScoreChange;

    public void AddScore(int t)
    {
        score += t;
        onScoreChange?.Invoke(score);
    }
}
