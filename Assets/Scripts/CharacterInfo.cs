using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public string characterId;
    [HideInInspector] public Vector3 originalScale;

    public static class DialogueData
    {
        public static string SelectedCharacterId;
    }

    void Awake()
    {
        originalScale = transform.localScale;
    }

}

