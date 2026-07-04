using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndingFadeManager : MonoBehaviour
{

    public Image endingFadeImage;
    public float endingFadeDuration = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        endingFadeImage.color = new Color(0f, 0f, 0f, 1f);
        endingFadeImage.raycastTarget = true;
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        yield return StartCoroutine(Fade(1f, 0f));
        endingFadeImage.raycastTarget = false;
    }

    IEnumerator Fade(float from, float to)
    {
        float timer = 0f;
        Color c = endingFadeImage.color;

        while (timer < endingFadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, timer / endingFadeDuration);
            endingFadeImage.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        endingFadeImage.color = new Color(c.r, c.g, c.b, to);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
