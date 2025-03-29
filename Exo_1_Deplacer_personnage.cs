using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Déplacement horizontal
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal * speed, rb2d.linearVelocity.y);
        rb2d.linearVelocity = movement;
    }
}