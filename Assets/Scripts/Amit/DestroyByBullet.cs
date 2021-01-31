using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBullet : MonoBehaviour
{

    private bool destroyBullet = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            Destroy(gameObject);
            if (destroyBullet)
            {
                Destroy(other.gameObject);
            }
        }
    }

    public void SetDestroyBullet(bool toggle)
    {
        destroyBullet = true;
    }
}
