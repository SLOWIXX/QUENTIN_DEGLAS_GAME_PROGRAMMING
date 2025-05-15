using UnityEngine;
using System.Collections.Generic;

public class PlayerEssence : MonoBehaviour
{
    public float stealthSpeedMultiplier = 1.5f;
    public LayerMask enemyLayer;
    public SpriteRenderer playerSprite;
    public Collider2D playerCollider;

    private bool isInEssence = false;
    private HashSet<Collider2D> essenceZones = new HashSet<Collider2D>();
    private Player_Controller playerController;
    private Rigidbody2D rb;

    void Start()
    {
        playerController = GetComponent<Player_Controller>();
        rb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (isInEssence)
        {
            Vector2 newVelocity = rb.linearVelocity;
            newVelocity.x *= stealthSpeedMultiplier;
            rb.linearVelocity = newVelocity;

            playerSprite.enabled = false;
            playerCollider.enabled = false;

            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 0.5f, enemyLayer);
            foreach (Collider2D enemyCol in enemies)
            {
                if (enemyCol.transform.position.y > transform.position.y)
                {
                    EnemyHealth enemy = enemyCol.GetComponent<EnemyHealth>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(1);
                    }
                }
            }
        }
        else
        {
            playerSprite.enabled = true;
            playerCollider.enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EssenceZone"))
        {
            if (essenceZones.Add(other)) 
            {
                if (essenceZones.Count == 1) 
                {
                    EnterEssenceZone();
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("EssenceZone"))
        {
            if (essenceZones.Remove(other)) 
            {
                if (essenceZones.Count == 0) 
                {
                    ExitEssenceZone();
                }
            }
        }
    }

    public void EnterEssenceZone()
    {
        isInEssence = true;
        Debug.Log("Plongé dans l'essence !");
    }

    public void ExitEssenceZone()
    {
        isInEssence = false;
        Debug.Log("Sorti de l'essence.");
    }
}
