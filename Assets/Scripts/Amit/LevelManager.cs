using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindObjectOfType<Health>().Damage(100);
        }
    }
}
