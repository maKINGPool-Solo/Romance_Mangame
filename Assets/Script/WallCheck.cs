using UnityEngine;

public class WallCheck : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = -rb.linearVelocity * 0.5f; 
                
                collision.transform.position -= (collision.transform.position - transform.position).normalized * 0.2f;
            }
        }
    }
}