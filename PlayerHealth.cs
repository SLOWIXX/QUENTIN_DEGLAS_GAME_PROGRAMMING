using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;

    public float knockbackForce = 10f;
    public float invulnerabilityDuration = 1f;

    private bool isInvulnerable = false;
    private Rigidbody2D rb;

    public SpawnPoint spawnPoint;

    private void Start()
    {
        currentLives = maxLives;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isInvulnerable)
        {
            TakeDamage(1, collision.transform);
        }
    }

    void TakeDamage(int amount, Transform enemy)
    {
        currentLives -= amount;
        Debug.Log("Player took damage! Lives left: " + currentLives);

        Vector2 knockbackDir = (transform.position - enemy.position).normalized;
        rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);

        StartCoroutine(Invulnerability());

        if (currentLives <= 0)
        {
            Respawn();
        }
    }

    System.Collections.IEnumerator Invulnerability()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false;
    }

    public void Respawn()
{
    currentLives = maxLives; 

    if (spawnPoint != null && spawnPoint.isUnlocked)
    {
        Debug.Log("Respawning player at unlocked spawn point.");
        spawnPoint.RespawnPlayer();
    }
    else
    {
        Debug.Log("No unlocked spawn point found, respawning player at default position.");
        transform.position = Vector3.zero;
    }

    if (rb != null)
    {
        rb.linearVelocity = Vector2.zero;
    }
}
public int GetCurrentLives()
{
    return currentLives;
}

}

