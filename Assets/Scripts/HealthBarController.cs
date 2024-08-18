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
        Debug.Log("Health initialized: " + currentHealth);
        
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
        if (currentHealth < 0) currentHealth = 0;
        UpdateHealthBar();
        Debug.Log("Took damage: " + damage + ", Current Health: " + currentHealth);
    }

    public void Heal(float healAmount)
    {
        // Increase health
        currentHealth += healAmount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        UpdateHealthBar();
        Debug.Log("Healed: " + healAmount + ", Current Health: " + currentHealth);
    }

    private void UpdateHealthBar()
    {
        // Update the health bar UI
        float fillAmount = currentHealth / maxHealth;
        healthBarImage.fillAmount = fillAmount;
        Debug.Log($"Health bar updated: fillAmount = {fillAmount}, currentHealth = {currentHealth}, maxHealth = {maxHealth}");

    }

    public void SetMaxHealth(float health)
    {
        maxHealth = health;
        currentHealth = health;
        UpdateHealthBar();
        Debug.Log("Max health set: " + maxHealth);
    }

    public void SetHealth(float health)
    {
        currentHealth = health;
        UpdateHealthBar();
        Debug.Log("Health set: " + currentHealth);
    }
}