using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWon : MonoBehaviour
{
    public void LoadNextLevel()
    {
        Debug.Log("LOAD NEXT");
        GameManager.Instance.LoadNextLevel();
    }
}
