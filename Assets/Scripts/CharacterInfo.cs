using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public string characterId;
    [HideInInspector] public Vector3 originalScale;

    public Transform[] spawnPoints;
    public Sprite[] poseSprites;

    private SpriteRenderer sr;

    private Collider2D col;

    public static class DialogueData
    {
        public static string SelectedCharacterId;
    }

    void Awake()
    {
        originalScale = transform.localScale;
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        Randomize();
    }

    void OnEnable()
    {
        FadeManager.OnScreenDarkened += Randomize;
        TimeManager.OnDayChanged += UpdateInteractable;
    }

    void OnDisable()
    {
        FadeManager.OnScreenDarkened -= Randomize;
        TimeManager.OnDayChanged -= UpdateInteractable;
    }

    void Randomize()
    {
        UpdateInteractable();
        PlaceRandomly();
        SetRandomPose();
    }

    void UpdateInteractable()
    {
        bool failed = CharacterProgress.IsFailed(characterId);

        if (col != null) col.enabled = !failed;

        /*if (sr != null)
        {
            Color c = sr.color;
            c.a = failed ? 0.4f : 1f;
            sr.color = c;
        }*/
    }

    void PlaceRandomly()
    {
        if (spawnPoints != null && spawnPoints.Length > 0)
        {
            int index = Random.Range(0, spawnPoints.Length);
            transform.position = spawnPoints[index].position;
        }
    }

    void SetRandomPose()
    {
        if(poseSprites != null && poseSprites.Length > 0)
        {
            int index = Random.Range(0, poseSprites.Length);
            sr.sprite = poseSprites[index];
        }
    }
}

