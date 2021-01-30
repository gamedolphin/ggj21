using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Sprite muzzleFlashSprite;
    [SerializeField] GameObject impactFX;
    [SerializeField] int muzzleFlashFrames = 3;

    Sprite defaultSprite;
    Color defaultColor;
    SpriteRenderer spriteRenderer;

    Shoot shootComponent;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        defaultSprite = spriteRenderer.sprite;
        defaultColor = spriteRenderer.color;
    }

    private void Start()
    {
        shootComponent = FindObjectOfType<Shoot>();
        StartCoroutine(ShowMuzzleFlash());
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(impactFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        shootComponent.IsBulletActive = false;
    }


}
