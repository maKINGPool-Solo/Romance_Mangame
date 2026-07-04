using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public string characterId;
    [HideInInspector] public Vector3 originalScale;

    public Transform[] spawnPoints;
    public Sprite[] poseSprites;

    private SpriteRenderer sr;

    public static class DialogueData
    {
        public static string SelectedCharacterId;
    }

    void Awake()
    {
        originalScale = transform.localScale;
        sr = GetComponent<SpriteRenderer>();

        Randomize();
    }

    void OnEnable()
    {
        FadeManager.OnScreenDarkened += Randomize;
    }

    void OnDisable()
    {
        FadeManager.OnScreenDarkened -= Randomize;
    }

    void Randomize()
    {
        PlaceRandomly();
        SetRandomPose();
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

