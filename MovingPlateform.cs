using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 pointA;           
    public Vector3 pointB;           
    public float speed = 2f;        
    private Vector3 target;         

    private Rigidbody2D rb;

    private void Start()
    {
        target = pointB; 

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            target = (target == pointA) ? pointB : pointA;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(pointA, 0.1f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(pointB, 0.1f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pointA, pointB);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null && playerRb.linearVelocity.y == 0)
            {
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 0);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
