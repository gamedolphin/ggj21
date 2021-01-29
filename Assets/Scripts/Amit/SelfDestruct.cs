using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float selfDestructTime = 2f;

    private void Start()
    {
        Destroy(gameObject, selfDestructTime);
    }
}
