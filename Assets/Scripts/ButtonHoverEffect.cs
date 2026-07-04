using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float normalAlpha = 0.6f;
    public float hoverAlpha = 1f;
    public float normalScale = 1f;
    public float hoverScale = 1.1f;
    public float transitionDuration = 0.15f;

    private CanvasGroup canvasGroup;
    private Coroutine currentRoutine;


    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = normalAlpha;
        transform.localScale = Vector3.one * normalScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentRoutine != null) StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(Transition(hoverAlpha, hoverScale));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentRoutine != null) StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(Transition(normalAlpha, normalScale));
    }

    IEnumerator Transition(float targetAlpha, float targetScale)
    {
        float startAlpha = canvasGroup.alpha;
        float startScale = transform.localScale.x;
        float timer = 0f;

        while (timer < transitionDuration)
        {
            timer += Time.deltaTime;
            float t = timer / transitionDuration;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            float scale = Mathf.Lerp(startScale, targetScale, t);
            transform.localScale = Vector3.one * scale;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
        transform.localScale = Vector3.one * targetScale;
    }
}
