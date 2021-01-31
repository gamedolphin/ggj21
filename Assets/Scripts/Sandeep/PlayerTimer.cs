using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class PlayerTimer : MonoBehaviour
{
    private float sinceLast = 0f;

    private Image mask;

    [SerializeField]
    private Image baseImage;

    public Color endColor;
    public Color fullColor;

    private void Awake()
    {
        mask = GetComponent<Image>();
        baseImage.color = fullColor;
    }


    private void Update()
    {
        float targetTimeout = GameManager.Instance.flashTime;

        if (Input.GetButton("Jump") && (Time.time - targetTimeout > sinceLast || sinceLast == 0))
        {
            mask.DOFillAmount(0, 0.2f);
            baseImage.DOColor(endColor, 0.2f);
            mask.DOFillAmount(1, targetTimeout).SetDelay(0.2f).SetEase(Ease.Linear);
            baseImage.DOColor(fullColor, targetTimeout).SetDelay(0.2f).SetEase(Ease.Linear);

            sinceLast = Time.time;
        }
    }
}
