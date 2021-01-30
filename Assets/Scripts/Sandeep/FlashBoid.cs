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

    private void Awake()
    {
        boid = GetComponent<Boid>();
        sp = GetComponent<SpriteRenderer>();
        originalColor = sp.color;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButton("Jump") && boid.IsBad)
        {
            sp.DOColor(highlightColor, 0.25f).SetOptions(false)
                .SetEase(Ease.InFlash, 2, 0)
                .SetLoops(5, LoopType.Yoyo)
                .OnComplete(DoneFlashing);
        }
    }

    private void DoneFlashing()
    {
        sp.color = originalColor;
    }
}
