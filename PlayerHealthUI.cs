using TMPro;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    public PlayerHealth playerHealth;
    private TextMeshProUGUI healthText;

    void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (playerHealth != null)
        {
            healthText.text = "PV : " + playerHealth.GetCurrentLives();
        }
    }
}
