using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleFadeManager : MonoBehaviour
{
    public Image titleFadeImage;
    public float titleFadeDuration = 1f;
    public static event System.Action OnTitleFadeInComplete;

    void Start()
    {
        titleFadeImage.color = new Color(0f, 0f, 0f, 1f);
        titleFadeImage.raycastTarget = true;
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        yield return StartCoroutine(Fade(1f, 0f));
        titleFadeImage.raycastTarget = false;
        OnTitleFadeInComplete?.Invoke();
    }

    public void FadeToMainScene(string sceneName)
    {
        StartCoroutine(FadeOutRoutine(sceneName));
    }

    IEnumerator FadeOutRoutine(string sceneName)
    {
        titleFadeImage.raycastTarget = true;
        yield return StartCoroutine(Fade(0f, 1f));
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator Fade(float from, float to)
    {
        float timer = 0f;
        Color c = titleFadeImage.color;

        while (timer < titleFadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, timer / titleFadeDuration);
            titleFadeImage.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        titleFadeImage.color = new Color(c.r, c.g, c.b, to);
    }


}
