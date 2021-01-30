using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shoot : MonoBehaviour
{

    [Header("Object References")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform reticule;
    [SerializeField] TextMeshProUGUI ammoText;

    [Header("Config Parameters")]
    [SerializeField] float bulletSpeed = 20f;
    [SerializeField] int maxAmmo = 20;

    int currentAmmo;
    PauseMenu pauseMenu;

    private void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
        currentAmmo = maxAmmo;
        UpdateAmmoText();
    }

    private void Update()
    {
        if (pauseMenu.IsGamePaused()) return;

        LookAtMousePosition();
        if(Input.GetMouseButtonDown(0) && HasAmmo())
        {
            ShootBullet();
            ReduceAmmo(1);
        }
    }

    private void LookAtMousePosition()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = (mousePos - new Vector2(transform.position.x, transform.position.y)).normalized;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90;
        transform.eulerAngles = new Vector3(0f, 0f, angle);
    }

    private void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, reticule.position, transform.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
    }

    private bool HasAmmo()
    {
        return currentAmmo > 0;
    }

    private void ReduceAmmo(int count)
    {
        currentAmmo -= count;
        UpdateAmmoText();
    }

    private void UpdateAmmoText()
    {
        ammoText.text = string.Format("Ammo: {0}", currentAmmo);
    }

}
 