using UnityEngine;

public class EnemyHealthUI : MonoBehaviour
{
    public EnemyPatrol enemy; 
    private Vector2 screenPos;

    void Update()
    {
        screenPos = Camera.main.WorldToScreenPoint(enemy.transform.position);
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        style.normal.textColor = Color.white;

        GUI.Label(new Rect(screenPos.x - 25, Screen.height - screenPos.y - 50, 100, 20), "PV : " + enemy.GetCurrentLives(), style);
    }
}
