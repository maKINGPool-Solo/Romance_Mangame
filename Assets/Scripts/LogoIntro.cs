using UnityEngine;
using System.Collections;

public class LogoIntro : MonoBehaviour
{
    public float duration = 0.6f;
    public float overshoot = 1.2f;
    public float buttonFadeDuration = 0.4f;

    public GameObject buttonGroup;
    private CanvasGroup buttonCanvasGroup;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.localScale = Vector3.zero;
        if (buttonGroup != null)
        {
            buttonCanvasGroup = buttonGroup.GetComponent<CanvasGroup>();
            if(buttonCanvasGroup == null)
            {
                buttonCanvasGroup = buttonGroup.AddComponent<CanvasGroup>();
            }
            buttonCanvasGroup.alpha = 0f;
            buttonCanvasGroup.interactable = false;
            buttonCanvasGroup.blocksRaycasts = false;
            buttonGroup.SetActive(true);
        }
    }

    void OnEnable()
    {
        TitleFadeManager.OnTitleFadeInComplete += StartLogoIntro;
    }

    void OnDisable()
    {
        TitleFadeManager.OnTitleFadeInComplete -= StartLogoIntro;
    }

    void StartLogoIntro()
    {
        StartCoroutine(LogoPunchIn());
    }

    IEnumerator LogoPunchIn()
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            float scale = BackEaseOut(t);
            transform.localScale = Vector3.one * scale;
            yield return null;
        }

        transform.localScale = Vector3.one;

        yield return StartCoroutine(FadeInButtons());
    }

    IEnumerator FadeInButtons()
    {
        float timer = 0f;

        while (timer < buttonFadeDuration)
        {
            timer += Time.deltaTime;
            buttonCanvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / buttonFadeDuration);
            yield return null;
        }

        buttonCanvasGroup.alpha = 1f;
        buttonCanvasGroup.interactable = true;
        buttonCanvasGroup.blocksRaycasts = true;
    }


    float BackEaseOut(float t)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1f;
        return 1f + c3 * Mathf.Pow(t - 1f, 3f) + c1 * Mathf.Pow(t - 1f, 2f);
    }


}
