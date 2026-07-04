using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 5f; 

    public float minX = -8.3f;
    public float maxX = 8.3f;
    public float minY = -4.7f;
    public float maxY = 4.7f;

    private Rigidbody2D rb; // 🟢 Rigidbody2D를 담을 변수 추가

    void Start()
    {
        // 🟢 시작할 때 오브젝트에 달린 Rigidbody2D를 자동으로 가져옵니다.
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() 
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        // 조작 감지 연출용 (GO! 타이틀 끄기)
        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed ||
            keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed ||
            keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed ||
            keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)
        {
            GameManager.instance.StartGameAction();
        }

        // 🟢 새로운 이동 로직: 방향 값을 저장할 변수
        Vector2 moveInput = Vector2.zero;

        // 1. 좌우 입력 체크
        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
        {
            moveInput.x = 1f;
        }
        else if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
        {
            moveInput.x = -1f;
        }

        // 2. 상하 입력 체크
        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
        {
            moveInput.y = 1f;
        }
        else if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)
        {
            moveInput.y = -1f;
        }

        // 🟢 [핵심] 강제 좌표 대입 대신, Rigidbody2D에 속도를 주어 물리적으로 이동시킵니다!
        // 이렇게 하면 물리 엔진이 벽(Collider)을 감지해서 꽉 막아줍니다.
        if (rb != null)
        {
            rb.linearVelocity = moveInput * moveSpeed;
        }

        // 🌟 화면 밖 탈출 방지용 Clamp는 기존 물리와 충돌하지 않게 position으로 유지합니다.
        Vector3 clampedPos = transform.position;
        clampedPos.x = Mathf.Clamp(clampedPos.x, minX, maxX);
        clampedPos.y = Mathf.Clamp(clampedPos.y, minY, maxY);
        transform.position = clampedPos;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.CompareTag("Enemy"))
        {
           GameManager.instance.PlayerDied();
        }
        if (target.CompareTag("Goal"))
        {
            GameManager.instance.PlayerReachedGoal();
        }
    }
}
