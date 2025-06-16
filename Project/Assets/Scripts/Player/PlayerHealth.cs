using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float health;

    public void Die()
    {
        Debug.Log("Dead");
    }

    public void Heal(float heal)
    {
        health += heal;
        if (health > maxHealth)
            heal = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health < 0)
            Die();
    }
}
