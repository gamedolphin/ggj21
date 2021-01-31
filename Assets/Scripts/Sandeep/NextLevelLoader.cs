using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelLoader : MonoBehaviour
{
    public string NextLevelName = "";

    private void Start()
    {
        GameManager.Instance.onLoadNextLevel += LoadNextLevel;
    }

    private void OnDestroy()
    {
        GameManager.Instance.onLoadNextLevel -= LoadNextLevel;
    }

    private void LoadNextLevel()
    {
        Debug.Log("HEre");
        SceneManager.LoadScene(NextLevelName);
    }
}
