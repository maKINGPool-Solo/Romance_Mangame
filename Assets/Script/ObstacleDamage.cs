using UnityEngine;

public class ObstacleDamage : MonoBehaviour
{
    // 💡 GameManager가 이제 플레이어 위치와 하트를 다 관리하므로, 
    // Start 함수에서 위치를 따로 기억할 필요가 없어졌습니다! (Start는 과감히 삭제)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 부딪힌 오브젝트의 태그가 "Player" 라면
        if (collision.CompareTag("Player"))
        {
            Debug.Log("플레이어가 가시에 찔림 -> GameManager에게 알림!");

            // ⭐ [핵심 수정] 직접 위치를 바꾸지 않고, 게임 매니저의 하트 시스템 함수를 실행합니다!
            // 이 함수 한 줄이 하트도 깎고, 텔레포트도 시켜주고, 다 해줍니다.
            GameManager.instance.PlayerDied();
        }
    }
}
