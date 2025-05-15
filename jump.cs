using UnityEngine;

public class Jump : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpSpeed = 10f;
    public float jumpMaxDuration = 0.5f;

    [Header("Falling")]
    public float fallGravity = 2.5f;
    public float fallSpeedMax = 15f;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;

    private Rigidbody2D rb2d;
    private Dash dashScript;
    private bool isJumping = false;
    private float jumpTimeCounter;
    private bool isGrounded;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        dashScript = GetComponent<Dash>();
    }

    void Update()
    {
        if (dashScript != null && dashScript.IsDashing())
            return;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && Input.GetButtonDown("Jump"))
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
}
