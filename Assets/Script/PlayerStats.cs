using UnityEngine;
using UnityEngine.UI; // PENTING: Wajib ada untuk mengakses elemen UI
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("UI Reference")]
    public Image healthBarImage; // Tempat kita menaruh UI Healthbar nanti

    void Start()
    {
        // Set darah penuh saat game mulai
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    // Fungsi untuk menerima damage (panggil ini saat pemain terkena hit)
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // Pastikan darah tidak kurang dari 0
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            SceneManager.LoadScene("GameOver");
        }

        UpdateHealthUI();
    }

    // Fungsi untuk menyembuhkan (healing)
    public void Heal(float amount)
    {
        currentHealth += amount;

        // Pastikan darah tidak melebihi batas maksimum
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHealthUI();
    }

    // Fungsi inti untuk mengubah tampilan UI
    void UpdateHealthUI()
    {
        // Rumus: Darah Sekarang / Darah Maksimum
        if (healthBarImage != null)
        {
            healthBarImage.fillAmount = currentHealth / maxHealth;
        }
    }
}