using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public Image fadeImage;
    public TextMeshProUGUI dayAnnounceText;
    public float fadeDuration = 1f;
    public float textFadeDuration = 0.3f;
    public float textHoldDuration = 0.7f;

    void OnEnable()
    {
        TimeManager.OnDayChanged += HandleDayChanged;
        TimeManager.OnGameEnd += HandleGameEnd;
    }

    void OnDisable()
    {
        TimeManager.OnDayChanged -= HandleDayChanged;
        TimeManager.OnGameEnd -= HandleGameEnd;
    }

    void HandleDayChanged()
    {
        StartCoroutine(FadeRoutine());
    }

    void HandleGameEnd()
    {
        StartCoroutine(GameEndRoutine());
    }

    IEnumerator FadeRoutine()
    {
        fadeImage.raycastTarget = true;

        yield return StartCoroutine(Fade(fadeImage, 0f, 1f, fadeDuration));
        dayAnnounceText.text = "Day " + TimeManager.Instance.currentDay;
        yield return StartCoroutine(FadeText(0f, 1f));
        yield return new WaitForSeconds(textHoldDuration);
        yield return StartCoroutine(FadeText(1f, 0f));
        yield return StartCoroutine(Fade(fadeImage, 1f, 0f, fadeDuration));

        fadeImage.raycastTarget = false;
    }

    IEnumerator Fade(Image image, float from, float to, float duration)
    {
        float timer = 0f;
        Color c = image.color;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, timer / duration);
            image.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        image.color = new Color(c.r, c.g, c.b, to);
    }

    IEnumerator FadeText(float from, float to)
    {
        float timer = 0f;
        Color c = dayAnnounceText.color;

        while (timer < textFadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, timer / textFadeDuration);
            dayAnnounceText.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        dayAnnounceText.color = new Color(c.r, c.g, c.b, to);
    }

    IEnumerator GameEndRoutine()
    {
        fadeImage.raycastTarget = true;
        yield return StartCoroutine(Fade(fadeImage, 0f, 1f, fadeDuration)); // 어두워지기만

        Destroy(TimeManager.Instance.gameObject);
        //SceneManager.LoadScene("EndingScene"); // 나중에 이름 채우기
        Destroy(gameObject); // TimeUI_Canvas(FadeManager 포함) 자체도 파괴 -> 엔딩씬에서 흰 화면에서 시작하면서 페이드 아웃 되는 걸 기본으로 설정해두기...
    }

}
