using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jumpSpeed = 10f; // Vitesse initiale du saut
    public float jumpMaxDuration = 0.5f; // Durée maximale pendant laquelle le saut peut être maintenu
    public float fallGravity = 2.5f; // Gravité appliquée lors de la chute
    public float fallSpeedMax = 15f; // Vitesse maximale de chute

    private Rigidbody2D rb2d;
    private bool isJumping = false;
    private float jumpTimeCounter;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Détection de l'entrée pour le saut
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            isJumping = true;
            jumpTimeCounter = jumpMaxDuration;
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpSpeed);
        }

        // Maintien du saut si le bouton est maintenu
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

        // Arrêt du saut si le bouton est relâché
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        // Appliquer la gravité personnalisée lors de la chute
        if (rb2d.linearVelocity.y < 0)
        {
            rb2d.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallGravity - 1) * Time.deltaTime;

            // Limiter la vitesse de chute
            if (rb2d.linearVelocity.y < -fallSpeedMax)
            {
                rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, -fallSpeedMax);
            }
        }
    }

    // Vérifie si le personnage est au sol
    private bool IsGrounded()
    {
        // Utilise le script GroundCheck pour vérifier si le personnage est au sol
        GroundCheck groundCheck = GetComponent<GroundCheck>();
        return groundCheck != null && groundCheck.IsGrounded;
    }
}