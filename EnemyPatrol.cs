using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public int pv = 3; 
    public float speed = 2f;
    [SerializeField, Range(0.1f, 50f)] private float limiteDroite = 1f;
    [SerializeField, Range(0.1f, 50f)] private float limiteGauche = 1f;

    private Vector3 limiteDroitePosition;
    private Vector3 limiteGauchePosition;
    private Rigidbody2D rb;
    private float direction = 1f;
    private SpriteRenderer skin;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        skin = GetComponent<SpriteRenderer>();

        limiteDroitePosition = transform.position + new Vector3(limiteDroite, 0, 0);
        limiteGauchePosition = transform.position - new Vector3(limiteGauche, 0, 0);
    }

    void Update()
    {
        if (Mathf.Abs(rb.linearVelocity.x) < 0.1f)
        {
            direction = -direction;
        }

        if (transform.position.x > limiteDroitePosition.x)
        {
            direction = -1f;
        }
        else if (transform.position.x < limiteGauchePosition.x)
        {
            direction = 1f;
        }

        skin.flipX = direction < 0f;

        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
    }

    public void TakeDamage(int amount)
    {
        pv -= amount;
        Debug.Log("Enemy took damage, PV restants : " + pv);

        if (pv <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            limiteDroitePosition = transform.position + new Vector3(limiteDroite, 0, 0);
            limiteGauchePosition = transform.position - new Vector3(limiteGauche, 0, 0);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawCube(limiteDroitePosition, new Vector3(0.2f, 1, 0.2f));
        Gizmos.DrawCube(limiteGauchePosition, new Vector3(0.2f, 1, 0.2f));
        Gizmos.DrawLine(limiteDroitePosition, limiteGauchePosition);
    }
    
    public int GetCurrentLives()
{
    return pv; 
}

}
