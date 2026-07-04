using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainSceneClickManager : MonoBehaviour
{
    public float hoverScaleMultiplier = 1.1f;
    public float scaleDuration = 0.15f;

    private CharacterInfo currentHovered;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MainMonologueManager.IsMonologueActive) return;

        HandleHover();

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            HandleClick();
        }
    }

    CharacterInfo GetCharacterUnderMouse()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

        if (hit.collider != null)
        {
            return hit.collider.GetComponent<CharacterInfo>();
        }
        return null;
    }

    void HandleHover()
    {
        CharacterInfo hovered = GetCharacterUnderMouse();

        if (hovered != currentHovered)
        {
            if (currentHovered != null)
            {
                StartCoroutine(ScaleRoutine(currentHovered.transform, currentHovered.originalScale));
            }

            if (hovered != null)
            {
                StartCoroutine(ScaleRoutine(hovered.transform, hovered.originalScale * hoverScaleMultiplier));
            }

            currentHovered = hovered;
        }
    }

    IEnumerator ScaleRoutine(Transform target, Vector3 targetScale)
    {
        Vector3 startScale = target.localScale;
        float timer = 0f;

        while (timer < scaleDuration)
        {
            timer += Time.deltaTime;
            target.localScale = Vector3.Lerp(startScale, targetScale, timer / scaleDuration);
            yield return null;
        }

        target.localScale = targetScale;
    }

    void HandleClick()
    {
        CharacterInfo character = GetCharacterUnderMouse();
        if (character != null)
        {
            Debug.Log("Clicked: " + character.characterId);
            DialogueData.SelectedCharacterId = character.characterId;
            FadeManager.Instance.FadeToScene("Dialogue_Scene", Color.white);
        }
    }

}
