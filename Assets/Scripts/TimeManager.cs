using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    public int currentDay = 1;
    
    [SerializeField]
    private float dayDuration = 360f;

    private float elapsedTime = 0f;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI dayText;

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
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= dayDuration)
        {
            elapsedTime = 0f;
            currentDay++;

            if(currentDay > 3)
            {
                Debug.Log("Ending");
                //SceneManager.LoadScene("");
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
}
