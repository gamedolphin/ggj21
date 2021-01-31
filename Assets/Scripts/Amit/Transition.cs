using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Transition : MonoBehaviour
{
    [SerializeField] float initialDelay = 0f;
    [SerializeField] float transitionDuration = 1f;

    [SerializeField] Animator anim;

    public void StartTransition()
    {
        StartCoroutine(DoTransition(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator DoTransition(int sceneIndex)
    {
        yield return new WaitForSeconds(initialDelay);
        anim.SetTrigger("transition");
        yield return new WaitForSeconds(transitionDuration);
        SceneManager.LoadScene(sceneIndex);
    }
}
