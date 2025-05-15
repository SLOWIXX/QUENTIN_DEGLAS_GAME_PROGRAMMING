using UnityEngine;
using System.Collections;

public class Dash : MonoBehaviour
{
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 3f;
    public int maxDashes = 2;

    private int currentDashes;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private bool isDashing = false;
    private float dashCooldownTimer = 0f;
    private float dashRechargeTimer = 0f;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentDashes = maxDashes;
        Debug.Log("Dashes initialisés : " + currentDashes);
    }

    private void Update()
    {
        // Réduction du cooldown du dash
        if (dashCooldownTimer > 0f)
            dashCooldownTimer -= Time.deltaTime;

        // Recharge des dashes
        RechargeDashes();

        // Déclenchement du dash
        if (Input.GetMouseButtonDown(1) && currentDashes > 0 && !isDashing && dashCooldownTimer <= 0f)
        {
            StartCoroutine(PerformDash());
        }
    }

    private void RechargeDashes()
    {
        if (currentDashes < maxDashes)
        {
            dashRechargeTimer += Time.deltaTime;
            if (dashRechargeTimer >= 2f) // Recharge un dash toutes les 2 secondes
            {
                currentDashes++;
                dashRechargeTimer = 0f;
                Debug.Log("Un dash a été rechargé ! Total dashes : " + currentDashes);
            }
        }
        else
        {
            dashRechargeTimer = 0f; // Réinitialise le timer si les dashes sont au maximum
        }
    }

    private IEnumerator PerformDash()
    {
        isDashing = true;
        dashCooldownTimer = dashCooldown; // Applique le cooldown
        currentDashes--; // Consomme un dash
        Debug.Log("Dash effectué, Dashes restants : " + currentDashes);

        // Détermine la direction du dash
        float direction = spriteRenderer.flipX ? -1f : 1f;

        // Désactive la gravité pendant le dash
        float originalGravity = rb2d.gravityScale;
        rb2d.gravityScale = 0;

        // Applique la vitesse du dash
        rb2d.linearVelocity = new Vector2(direction * dashSpeed, 0f);

        // Attend la fin de la durée du dash
        yield return new WaitForSeconds(dashDuration);

        // Réactive la gravité et arrête le mouvement
        rb2d.gravityScale = originalGravity;
        rb2d.linearVelocity = Vector2.zero;
        isDashing = false;
    }

    public void AddDash()
    {
        if (currentDashes < maxDashes)
        {
            currentDashes++;
            Debug.Log("Un dash supplémentaire ! Total dashes : " + currentDashes);
        }
        else
        {
            Debug.LogWarning("Le nombre maximum de dashes est atteint. Dashes actuels : " + currentDashes);
        }
    }

    public bool IsDashing()
    {
        return isDashing;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Torch"))
        {
            AddDash(); // Ajoute un dash lorsqu'une torche est collectée
            Destroy(other.gameObject); // Détruit la torche
        }
    }
}