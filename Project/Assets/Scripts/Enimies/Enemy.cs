using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected float maxHealth;
    [SerializeField]
    protected float health;
    [SerializeField]
    protected List<Gun> gunList;
    [SerializeField]
    protected float moveSpeed;

    protected void Shoot() { }
    protected void Move() { }

    public void Die()
    {
        Destroy(gameObject);
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
