using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StopGame : MonoBehaviour
{

    public GameObject levelWon;

    private void Start()
    {
        levelWon.SetActive(false);
        GameManager.Instance.onGameWin += OnGameWon;
    }

    private void OnGameWon(Vector3 pos)
    {
        StartCoroutine(ShowGameWon());
    }

    private IEnumerator ShowGameWon()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0;
        levelWon.SetActive(true);
    }
}
