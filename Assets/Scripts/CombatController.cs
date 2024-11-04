using UnityEngine;
using TMPro;

public class CombatController : MonoBehaviour
{
    public int playerHealth = 3;
    public int damageAmount = 1;

    public TextMeshProUGUI livesText;
    public GameObject deathMessage;

    void Start()
    {
        UpdateLivesText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 1.0f);

        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            hit.collider.GetComponent<Enemy>().TakeDamage(damageAmount);
        }
    }

    public void TakeDamage(int amount)
    {
        playerHealth -= amount;
        UpdateLivesText();

        if (playerHealth <= 0)
        {
            Die();
        }
    }

    void UpdateLivesText()
    {
        livesText.text = "Lives: " + playerHealth;
    }

    void Die()
    {
        deathMessage.SetActive(true);
        Time.timeScale = 0;
    }
}
