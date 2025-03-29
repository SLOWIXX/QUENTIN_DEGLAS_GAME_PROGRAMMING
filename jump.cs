using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jumpSpeed = 10f; 
    public float jumpMaxDuration = 0.5f; 
    public float fallGravity = 2.5f; 
    public float fallSpeedMax = 15f; 

    private Rigidbody2D rb2d;
    private bool isJumping = false;
    private float jumpTimeCounter;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            isJumping = true;
            jumpTimeCounter = jumpMaxDuration;
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpSpeed);
        }

        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpSpeed);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        if (rb2d.linearVelocity.y < 0)
        {
            rb2d.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallGravity - 1) * Time.deltaTime;

            if (rb2d.linearVelocity.y < -fallSpeedMax)
            {
                rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, -fallSpeedMax);
            }
        }
    }

    private bool IsGrounded()
    {
        GroundCheck groundCheck = GetComponent<GroundCheck>();
        return groundCheck != null && groundCheck.IsGrounded;
    }
}