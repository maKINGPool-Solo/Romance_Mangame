using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Vector3 playerInitialPosition; 
    private GameObject[] enemies; 
    private GameObject player; 

    // 🌟 [추가] 총 코인 개수와 현재 먹은 코인 개수 변수
    private int totalCoins = 0;
    private int collectedCoins = 0;

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

        // 🌟 [추가] 시작할 때 맵에 있는 "Coin" 태그의 개수를 자동으로 셉니다.
        totalCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
        Debug.Log("이 맵의 총 코인 개수: " + totalCoins);
    }

    // 🌟 [새로 만든 함수] 코인을 먹을 때마다 실행될 함수
    public void CoinCollected()
    {
        collectedCoins++;
        Debug.Log($"코인 획득! ({collectedCoins} / {totalCoins})");

        // 맵에 있는 코인을 다 먹었다면 클리어!
        if (collectedCoins >= totalCoins)
        {
            AllCoinsCollected();
        }
    }

    // 🌟 [새로 만든 함수] 모든 코인을 다 먹었을 때 실행되는 클리어 로직
    private void AllCoinsCollected()
    {
        Debug.Log("🎉 축하합니다! 모든 코인을 모아 클리어했습니다!");
        // 여기에 승리 팝업창을 띄우거나, 다음 씬으로 넘어가는 코드를 넣으면 됩니다.
        // 예: SceneManager.LoadScene("NextStage");
    }

    // (기존 골인 함수는 그대로 유지)
    public void PlayerReachedGoal()
    {
        player.transform.position = playerInitialPosition;
        player.GetComponent<PlayerScript>().moveSpeed += 0.3f;
        foreach(GameObject g in enemies)
        {
            g.GetComponent<EnemyScript>().moveSpeed += 1f; 
        }
    }

    public void PlayerDied()
    {
        Time.timeScale = 0f;
        StartCoroutine(RestartGame());
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}