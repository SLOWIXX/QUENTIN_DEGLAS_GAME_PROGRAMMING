using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float cooldown = 2f;

    private float lastFireTime = -Mathf.Infinity;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && Time.time >= lastFireTime + cooldown)
        {
            Shoot();
            lastFireTime = Time.time;
        }
    }

    void Shoot()
    {
        Vector2 inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (inputDirection == Vector2.zero)
        {
            bool isFacingLeft = GetComponent<SpriteRenderer>().flipX;
            inputDirection = isFacingLeft ? Vector2.left : Vector2.right;
        }

        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
        fireball.GetComponent<Fireball>().SetDirection(inputDirection.normalized);
    }
}
