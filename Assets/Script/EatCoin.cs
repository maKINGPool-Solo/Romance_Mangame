using UnityEngine;

public class EatCoin : MonoBehaviour
{
    [Header("--- 말풍선 고유 ID 설정 ---")]
    [Tooltip("위쪽 말풍선은 1, 아래쪽 말풍선은 2를 인스펙터에서 적어주세요.")]
    public int bubbleID; 

    [Header("--- 효과음 설정 ---")]
    public AudioClip eatSound; // 🔊 인스펙터에서 넣을 효과음 파일 칸

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 부딪혔을 때만 작동합니다.
        if (collision.CompareTag("Player"))
        {
            // GameManager에게 자기가 몇 번 말풍선인지 숫자를 던져주며 획득 처리를 요청합니다.
            if (GameManager.instance != null)
            {
                GameManager.instance.CoinCollected(bubbleID);
            }
            
            // 효과음이 설정되어 있다면 카메라 위치(귀 바로 앞)에서 소리를 재생합니다.
            if (eatSound != null)
            {
                // Z축 거리 한계를 무시하기 위해 메인 카메라의 위치를 가져옵니다.
                Vector3 spawnPos = Camera.main.transform.position;
                spawnPos.z += 1f; // 카메라 살짝 앞에 소리 배치
                
                AudioSource.PlayClipAtPoint(eatSound, spawnPos);
            }
            
            // 아이템을 먹었으므로 말풍선 오브젝트는 제거합니다.
            Destroy(gameObject);
        }
    }
}
