using UnityEngine;

public class EatCoin : MonoBehaviour
{
    [Header("--- 말풍선 고유 ID 설정 ---")]
    [Tooltip("위쪽 말풍선은 1, 아래쪽 말풍선은 2를 인스펙터에서 적어주세요.")]
    public int bubbleID; 

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
            
            // 아이템을 먹었으므로 말풍선 오브젝트는 제거합니다.
            Destroy(gameObject);
        }
    }
}