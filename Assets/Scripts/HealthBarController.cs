using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Image healthBarImage;
    private float currentHealth;
    private float maxHealth = 100f;
    private PlayerController _player;

    void Start()
    {
        // Initialize health
        currentHealth = maxHealth;
        UpdateHealthBar();
        
        //Get player
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if(_player != null)
        {
            // Subscribe to player onDamageEvent
            _player.onDamage += TakeDamage;
        }
    }

    private void OnDisable()
    {
        if (_player != null)
        {
            // Unsubscribe to player onDamageEvent
            _player.onDamage -= TakeDamage;
        }
    }

    public void TakeDamage(float damage)
    {
        // Reduce health
        currentHealth -= damage;
        if (currentHealth <= 0){ 
            FindObjectOfType<WinLoose>().LoseGame();
        }
        UpdateHealthBar();
    }

    public void Heal(float healAmount)
    {
        // Increase health
        currentHealth += healAmount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        // Update the health bar UI
        float fillAmount = currentHealth / maxHealth;
        healthBarImage.fillAmount = fillAmount;

    }

    public void SetMaxHealth(float health)
    {
        maxHealth = health;
        currentHealth = health;
        UpdateHealthBar();
    }

    public void SetHealth(float health)
    {
        currentHealth = health;
        UpdateHealthBar();
    }
}