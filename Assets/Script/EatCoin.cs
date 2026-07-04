using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 부딪힌 오브젝트가 플레이어라면
        if (collision.CompareTag("Player"))
        {
            // GameManager에게 코인 하나 먹었다고 알려줌
            GameManager.instance.CoinCollected();
            
            // 먹었으니 맵에서 코인 오브젝트를 삭제(파괴)함
            Destroy(gameObject);
        }
    }
}
