using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public GameObject healthBarPrefab;
    private Image healthBarFill;
    private GameObject healthBarInstance;

    void Start()
    {
        currentHealth = maxHealth;
        CreateHealthBar();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
    }

    void CreateHealthBar()
    {
        if (healthBarPrefab != null)
        {
            healthBarInstance = Instantiate(healthBarPrefab, transform);
            healthBarInstance.transform.localPosition = new Vector3(0, 2.5f, 0); 
            healthBarFill = healthBarInstance.GetComponentInChildren<Image>();
            UpdateHealthBar();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }
    }
}
