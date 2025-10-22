using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public int playerHealth = 8; // player dies after 8 hits

    public Text scoreText;
    public Text healthText;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start() => UpdateUI();

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    public void TakeDamage(int amount)
    {
        playerHealth -= amount;
        UpdateUI();

        if (playerHealth <= 0)
        {
            Debug.Log("Game Over!");
            // Optionally reload scene
        }
    }

    void UpdateUI()
    {
        if (scoreText != null) scoreText.text = "Score: " + score;
        if (healthText != null) healthText.text = "Health: " + playerHealth;
    }
}
