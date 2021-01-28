using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform reticule;
    [SerializeField] float bulletSpeed = 20f;

    private void Update()
    {
        LookAtMousePosition();
        if(Input.GetMouseButtonDown(0))
        {
            ShootBullet();
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
}
 