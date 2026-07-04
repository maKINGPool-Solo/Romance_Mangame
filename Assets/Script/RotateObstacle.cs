using UnityEngine;

public class RotateObstacle : MonoBehaviour
{
    
    public float rotateSpeed = 70f; 

    void Update()
    {
       
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
}
