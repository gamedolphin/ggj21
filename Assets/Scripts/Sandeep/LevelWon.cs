using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWon : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        scoreText.text = "SCORE : " + ScoreKeeper.Instance.score;
    }

    public void LoadNextLevel()
    {
        Debug.Log("LOAD NEXT");
        GameManager.Instance.LoadNextLevel();
    }
}
