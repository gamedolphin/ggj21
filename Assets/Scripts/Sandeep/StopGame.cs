using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StopGame : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.onGameWin += OnGameWon;
    }

    private void OnGameWon(Vector3 pos)
    {
    }
}
