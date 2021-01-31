using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWon : MonoBehaviour
{
    public void OnNextLevel()
    {
        GameManager.Instance.LoadNextLevel();
    }
}
