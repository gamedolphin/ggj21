using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatedBoid : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    private string[] animationNames;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();

        StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(1, 5));
            animator.Play(animationNames[Random.Range(0, animationNames.Length)]);
        }
    }
}
