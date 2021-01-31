using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] float timeToFadeButton = 0.5f;
    [SerializeField] Animator anim;

    Color32 startColor = new Color32(35, 66, 74, 255);

    public string FirstLevelName = "Level 1";

    private void Start()
    {
        buttonText.color = startColor;
    }

    public void StartSequence()
    {
        anim.SetTrigger("transition");
        StartCoroutine(FadeOutButton());
    }

    public void StartLoadLevelCoroutine()
    {
        StartCoroutine(LoadNextScene());
    }

    private IEnumerator FadeOutButton()
    {
        float timeScale = 0;
        Color32 endColor = new Color32(35, 66, 74, 0);

        while (timeScale < 1)
        {
            float lerpValue = Mathf.Max(timeScale, 0);
            buttonText.color = Color32.Lerp(startColor, endColor, lerpValue);
            timeScale += Time.deltaTime / timeToFadeButton;
            yield return null;
        }
    }
    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(timeToFadeButton + 0.2f);
        SceneManager.LoadScene(FirstLevelName);
    }
}
