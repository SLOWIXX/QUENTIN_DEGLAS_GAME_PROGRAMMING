using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Torch : MonoBehaviour
{
    private Light2D light2D;

    public float flickerAmount = 0.2f;
    public float flickerSpeed = 5f;

    private float baseIntensity;

    void Start()
    {
        light2D = GetComponent<Light2D>();
        baseIntensity = light2D.intensity;
    }

    void Update()
    {
        light2D.intensity = baseIntensity + Mathf.PerlinNoise(Time.time * flickerSpeed, 0f) * flickerAmount;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Dash dash = other.gameObject.GetComponent<Dash>();
            if (dash != null)
            {
                dash.AddDash(); 
                Destroy(gameObject); 
                Debug.Log("Le joueur a récupéré un deuxième dash !");
            }
        }
    }
}
