using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Boid), typeof(SpriteRenderer))]
public class FlashBoid : MonoBehaviour
{
    private Boid boid;
    private SpriteRenderer sp;

    public Color originalColor;
    public Color highlightColor;

    private float sinceLast = 0f;

    private void Awake()
    {
        boid = GetComponent<Boid>();
        sp = GetComponent<SpriteRenderer>();
        originalColor = sp.color;

        GameManager.Instance.onGameWin += OnGameWon;
    }

    private Tween flashTween;
    private Tween fadeTween;

    // Update is called once per frame
    private void Update()
    {

        if (!boid.IsBad)
        {
            return;
        }

        if (Input.GetButton("Jump") && Time.time - boid.TargetTimeout > sinceLast)
        {
            flashTween = sp.DOColor(highlightColor, 0.25f).SetOptions(false)
                .SetEase(Ease.InFlash, 2, 0)
                .SetLoops(5, LoopType.Yoyo)
                .OnComplete(DoneFlashing);

            transform.DOScale(1.5f, 1.5f);

            sinceLast = Time.time;
        }
    }

    private void OnDestroy()
    {
        if (flashTween != null )
        {
            flashTween.Kill();
        }

        if (fadeTween != null)
        {
            fadeTween.Kill();
        }

        GameManager.Instance.onGameWin -= OnGameWon;
    }

    private void DoneFlashing()
    {
        sp.color = originalColor;
        transform.DOScale(1, 0.25f);
    }

    private void OnGameWon(Vector3 pos)
    {
        fadeTween = sp.DOFade(0, 0.25f);
    }
}
