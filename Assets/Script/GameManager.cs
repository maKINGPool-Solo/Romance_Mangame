using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Vector3 playerInitialPosition; 
    private GameObject[] enemies; 
    private GameObject player; 

    private int totalCoins = 0;
    private int collectedCoins = 0;

    [Header("--- 하트 시스템 설정 ---")]
    public int maxHealth = 3;        
    private int currentHealth;       
    
    public Image[] heartImages;      
    public Sprite brokenHeartSprite; 
    public GameObject damageFlashUI;   // 피격 시 빨갛게 번쩍일 UI

    [Header("--- 스타트 연출 설정 ---")]
    public GameObject goTextUI;         
    private bool isGameStarted = false; 

    [Header("--- 클리어 / 실패 결과 UI ---")]
    public GameObject clearSuccessUI;   // 💖 성공 하트 UI
    public GameObject clearFailUI;      // ❌ 실패 X UI

    [Header("--- 대사 이미지 UI 설정 ---")]
    public GameObject dialogUI1;        // 💬 왼쪽 위 "아니 그런게 아니고.." (통이미지)
    public GameObject dialogUI2;        // 💬 오른쪽 아래 "내가 사실은..." (통이미지)

    void Awake()
    {
        if(instance == null)
            instance = this; 
    }

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindWithTag("Player");

        if (player != null)
            playerInitialPosition = player.transform.position;
        
        Time.timeScale = 1f; 

        totalCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
        Debug.Log("이 맵의 총 말풍선 개수: " + totalCoins);

        currentHealth = maxHealth;

        // 모든 결과 및 대사 UI들의 알파값 초기화 및 비활성화
        InitUIAlpha(clearSuccessUI);
        InitUIAlpha(clearFailUI);
        InitUIAlpha(dialogUI1);
        InitUIAlpha(dialogUI2);
        InitUIAlpha(damageFlashUI);

        if (goTextUI != null) goTextUI.SetActive(true);
    }

    private void InitUIAlpha(GameObject uiObj)
    {
        if (uiObj != null)
        {
            Image img = uiObj.GetComponent<Image>();
            if (img != null)
            {
                Color c = img.color;
                c.a = 0f;
                img.color = c;
            }
            uiObj.SetActive(false);
        }
    }

    public void StartGameAction()
    {
        if (isGameStarted) return; 
        isGameStarted = true;
        if (goTextUI != null) StartCoroutine(FadeOutCoroutine(goTextUI, 0.3f, true)); 
    }

    // 💬 말풍선을 먹을 때 호출되는 함수 (몇 번 말풍선인지 ID를 받아옵니다)
    public void CoinCollected(int bubbleID)
    {
        collectedCoins++;
        Debug.Log($"말풍선 {bubbleID}번 획득! ({collectedCoins} / {totalCoins})");

        // 1번(위쪽)을 먹으면 대사창1, 2번(아래쪽)을 먹으면 대사창2를 페이드 연출로 실행
        if (bubbleID == 1) 
        {
            StartCoroutine(ShowDialogSequence(dialogUI1, 2f));
        }
        else if (bubbleID == 2)
        {
            StartCoroutine(ShowDialogSequence(dialogUI2, 2f));
        }
    }

    // 대사 이미지가 스르륵 나타났다 2초 대기 후 사라지는 연출 코루틴
    IEnumerator ShowDialogSequence(GameObject dialogObj, float delay)
    {
        if (dialogObj == null) yield break;

        // 1. 나타나기 (0.3초 동안 페이드 인)
        yield return StartCoroutine(FadeInCoroutine(dialogObj, 0.3f));

        // 2. 2초간 화면에 유지
        yield return new WaitForSeconds(delay);

        // 3. 사라지기 (0.5초 동안 페이드 아웃 후 비활성화)
        yield return StartCoroutine(FadeOutCoroutine(dialogObj, 0.5f, true));
    }

    public void PlayerReachedGoal()
    {
        if (collectedCoins >= totalCoins)
        {
            Debug.Log("🎉 미션 성공!");
            if (clearSuccessUI != null) StartCoroutine(FadeInCoroutine(clearSuccessUI, 0.5f));
            Time.timeScale = 0f; 
        }
        else
        {
            Debug.Log("❌ 탈출 실패!");
            StartCoroutine(ClearFailSequence());
        }
    }

    IEnumerator ClearFailSequence()
    {
        if (player != null)
        {
            player.transform.position = playerInitialPosition;
            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
            if (playerRb != null) playerRb.linearVelocity = Vector2.zero;
        }

        yield return StartCoroutine(FadeInCoroutine(clearFailUI, 0.3f));
        yield return new WaitForSecondsRealtime(0.8f);
        yield return StartCoroutine(FadeOutCoroutine(clearFailUI, 0.4f, false));
    }

    public void PlayerDied()
    {
        if (player == null) return;

        currentHealth--;
        Debug.Log($"플레이어 피격! 남은 목숨: {currentHealth}");

        if (damageFlashUI != null) StartCoroutine(DamageFlashSequence());

        if (currentHealth >= 0 && currentHealth < heartImages.Length)
        {
            StartCoroutine(HeartBreakSequence(heartImages[currentHealth]));
        }

        if (currentHealth > 0)
        {
            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
            if (playerRb != null) playerRb.linearVelocity = Vector2.zero;
            player.transform.position = playerInitialPosition;
        }
        else
        {
            Debug.Log("💀 게임 오버! 씬을 재시작합니다.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    IEnumerator HeartBreakSequence(Image heartImg)
    {
        if (heartImg == null) yield break;

        float duration = 0.15f; 
        float elapsed = 0f;
        Color c = heartImg.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Clamp01(1f - (elapsed / duration));
            heartImg.color = c;
            yield return null;
        }

        heartImg.sprite = brokenHeartSprite;

        elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsed / duration);
            heartImg.color = c;
            yield return null;
        }
        c.a = 1f;
        heartImg.color = c;
    }

    IEnumerator DamageFlashSequence()
    {
        damageFlashUI.SetActive(true);
        Image img = damageFlashUI.GetComponent<Image>();
        Color c = img.color;
        
        float elapsed = 0f;
        while (elapsed < 0.1f)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 0.4f, elapsed / 0.1f);
            img.color = c;
            yield return null;
        }

        elapsed = 0f;
        while (elapsed < 0.2f)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Lerp(0.4f, 0f, elapsed / 0.2f);
            img.color = c;
            yield return null;
        }
        c.a = 0f;
        img.color = c;
        damageFlashUI.SetActive(false);
    }

    IEnumerator FadeInCoroutine(GameObject uiObj, float duration)
    {
        Image img = uiObj.GetComponent<Image>();
        if (img == null) yield break;

        uiObj.SetActive(true);
        float elapsed = 0f;
        Color c = img.color;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime; 
            c.a = Mathf.Clamp01(elapsed / duration);
            img.color = c;
            yield return null;
        }
        c.a = 1f;
        img.color = c;
    }

    IEnumerator FadeOutCoroutine(GameObject uiObj, float duration, bool disableOnEnd)
    {
        Image img = uiObj.GetComponent<Image>();
        if (img == null) yield break;

        float elapsed = 0f;
        Color c = img.color;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            c.a = Mathf.Clamp01(1f - (elapsed / duration));
            img.color = c;
            yield return null;
        }
        c.a = 0f;
        img.color = c;

        if (disableOnEnd) uiObj.SetActive(false);
    }
}