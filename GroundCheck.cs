using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;
    public float groundCheckOffset = -0.5f;

    private bool isGrounded;

    private void FixedUpdate()
    {
        Vector2 groundCheckPosition = new Vector2(transform.position.x, transform.position.y + groundCheckOffset);
        isGrounded = Physics2D.OverlapCircle(groundCheckPosition, groundCheckRadius, groundLayer);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 20), isGrounded ? "On Ground" : "In Air");
    }

    
}