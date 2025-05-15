using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset; 

    void LateUpdate()
    {
        transform.position = player.position + offset;

        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
