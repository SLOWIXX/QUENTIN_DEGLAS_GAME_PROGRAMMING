using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float baseSpeed = 5f;
    public float speedMultiplier = 1f;

    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private Dash dashScript;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        dashScript = GetComponent<Dash>();
        rb2d.freezeRotation = true;
    }

    void FixedUpdate()
    {
        if (dashScript != null && dashScript.IsDashing()) return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal * baseSpeed * speedMultiplier, rb2d.linearVelocity.y);
        rb2d.linearVelocity = movement;

        if (moveHorizontal > 0)
            spriteRenderer.flipX = false;
        else if (moveHorizontal < 0)
            spriteRenderer.flipX = true;
    }
}
