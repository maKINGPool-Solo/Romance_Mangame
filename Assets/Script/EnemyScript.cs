using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float moveSpeed = 4f; 

    // 🔍 [SerialzeField]의 오타를 [SerializeField]로 올바르게 고쳤습니다!
    [SerializeField]
    private bool moveLeft; 

    void Update()
    {
        Vector3 temp = transform.position;

        if (moveLeft)
        {
            temp.x -= moveSpeed * Time.deltaTime;
        }
        else 
        {
            temp.x += moveSpeed * Time.deltaTime; 
        }

        // 중복되던 transform.position 반영 코드를 조건문 밖으로 한 번만 쓰도록 합쳤습니다.
        transform.position = temp; 
    }

   
}