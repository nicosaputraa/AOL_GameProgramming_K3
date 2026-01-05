using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("UI Reference")]
    public Image healthBarImage; 

    void Start()
    {
        
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            SceneManager.LoadScene("GameOver");
        }

        UpdateHealthUI();
    }

    
    public void Heal(float amount)
    {
        currentHealth += amount;

        
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHealthUI();
    }

    
    void UpdateHealthUI()
    {
        
        if (healthBarImage != null)
        {
            healthBarImage.fillAmount = currentHealth / maxHealth;
        }
    }
}