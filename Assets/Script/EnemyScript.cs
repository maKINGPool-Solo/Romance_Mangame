using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float moveSpeed = 4f; 

    [Header("--- 이동 방향 설정 ---")]
    [Tooltip("기본 방향입니다. 예: 우측(1,0), 대각선 우하향(1,-1)")]
    public Vector2 moveDirection = new Vector2(1f, 0f);

    // 현재 진짜 움직이고 있는 방향을 저장하는 변수
    private Vector3 currentVelocity;

    void Start()
    {
        // 방향 벡터의 크기를 1로 예쁘게 맞춰준 뒤, 첫 이동 방향으로 설정합니다.
        moveDirection.Normalize();
        currentVelocity = moveDirection;
    }

    void Update()
    {
        // 1. 벽에 부딪히기 전까지는 현재 방향으로 속도에 맞춰 무조건 직진!
        transform.position += currentVelocity * moveSpeed * Time.deltaTime;

        // 2. 표창이니까 제자리에서 팽이처럼 빙글빙글 돌기
        transform.Rotate(0, 0, 300f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 오직 구름 외곽 벽(Could_Hard)에 부딪혔을 때만!
        if (collision.name.Contains("Could_Hard"))
        {
            // 가던 방향을 정확히 정반대로 뒤집어버립니다 (180도 턴)
            currentVelocity = -currentVelocity;
        }
    }
}