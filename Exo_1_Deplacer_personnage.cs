using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal * speed, rb2d.linearVelocity.y);
        rb2d.linearVelocity = movement;


        if (moveHorizontal > 0)
        {
            spriteRenderer.flipX = false; 
        }
        else if (moveHorizontal < 0)
        {
            spriteRenderer.flipX = true; 
        }
    }
}