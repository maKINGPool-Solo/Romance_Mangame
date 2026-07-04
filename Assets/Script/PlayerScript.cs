using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 5f; 

    // 🌟 화면 크기에 맞게 이 값을 유니티 인스펙터 창에서 조금씩 조절해 보세요!
    public float minX = -8.3f;
    public float maxX = 8.3f;
    public float minY = -4.7f;
    public float maxY = 4.7f;

    void Update() 
    {
        Vector2 temp = transform.position;
        var keyboard = Keyboard.current;
        
        if (keyboard == null) return;

        float deltaTime = Time.deltaTime;

        // 1. 좌우 이동
        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
        {
            temp.x += moveSpeed * deltaTime;
        }
        else if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
        {
            temp.x -= moveSpeed * deltaTime;
        }

        // 2. 상하 이동
        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
        {
            temp.y += moveSpeed * deltaTime;
        }
        else if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)
        {
            temp.y -= moveSpeed * deltaTime;
        }

        // 🌟 [핵심] 화면 밖으로 나가지 못하게 x축과 y축 좌표를 제한합니다.
        temp.x = Mathf.Clamp(temp.x, minX, maxX);
        temp.y = Mathf.Clamp(temp.y, minY, maxY);

        transform.position = temp;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Enemy")
        {
           GameManager.instance.PlayerDied();

        }
        
    }
}