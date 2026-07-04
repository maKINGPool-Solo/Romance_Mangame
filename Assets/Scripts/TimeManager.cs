using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    public static event System.Action OnDayChanged;
    public static event System.Action OnGameEnd;
    public GameObject timerBackgroundImage;

    public int currentDay = 1;
    
    [SerializeField]
    private float dayDuration = 360f;

    private float elapsedTime = 0f;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI dayText;

    public bool isPaused = true;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (isPaused)
        {
            return;
        }

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= dayDuration)
        {
            elapsedTime = 0f;
            currentDay++;

            if (currentDay > 3)
            {
                timeText.gameObject.SetActive(false);
                dayText.gameObject.SetActive(false);
                OnGameEnd?.Invoke();
            }
            else
            {
                OnDayChanged?.Invoke();
            }
        }

        UpdateTimeUI();
    }

    void UpdateTimeUI()
    {
        float remainTime = dayDuration - elapsedTime;
        int minutes = Mathf.FloorToInt(remainTime / 60);
        int seconds = Mathf.FloorToInt(remainTime % 60);

        if (timeText != null && dayText != null) {
            timeText.text = $"{minutes:00}:{seconds:00}";
            dayText.text = $"Day {currentDay}";
        }
    }

    public void SetTimeUIVisible(bool visible)
    {
        timeText.enabled = visible;
        if (timerBackgroundImage != null)
        {
            timerBackgroundImage.SetActive(visible);
        }
    }
}
