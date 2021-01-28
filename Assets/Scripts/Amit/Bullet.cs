using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Sprite muzzleFlashSprite;
    [SerializeField] int muzzleFlashFrames = 3;

    Sprite defaultSprite;
    Color defaultColor;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        defaultSprite = spriteRenderer.sprite;
        defaultColor = spriteRenderer.color;
    }

    private void Start()
    {
        StartCoroutine(ShowMuzzleFlash());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private IEnumerator ShowMuzzleFlash()
    {
        spriteRenderer.sprite = muzzleFlashSprite;
        spriteRenderer.color = Color.white;

        for (int i = 0; i < muzzleFlashFrames; i++)
        {
            yield return null;
        }

        spriteRenderer.sprite = defaultSprite;
        spriteRenderer.color = defaultColor;
    }
}
