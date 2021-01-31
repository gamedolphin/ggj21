using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoLoadNextScene : MonoBehaviour
{
    public float LoadTime = 2;
    public string NextSceneName = "";

    public void Start()
    {
        StartCoroutine(StartNextLevel());
    }

    private IEnumerator StartNextLevel()
    {
        yield return new WaitForSeconds(LoadTime);
        SceneManager.LoadScene(NextSceneName);
    }
}
