using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool isUnlocked = false;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("Player not found! Ensure the Player GameObject has the 'Player' tag.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isUnlocked)
        {
            UnlockSpawnPoint();
        }
    }

    public void UnlockSpawnPoint()
    {
        isUnlocked = true;
        Debug.Log("Spawn point unlocked!");
    }

    public void RespawnPlayer()
    {
        if (player != null)
        {
            player.position = transform.position; 
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero; 
            }
        }
        else
        {
            Debug.LogError("Player object is null during respawn.");
        }
    }
}
