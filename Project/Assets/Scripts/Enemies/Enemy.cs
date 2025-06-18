using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public abstract class Enemy : MonoBehaviour, IDamageable
{
    [Header("GeneralEnemySettings")]
    [SerializeField] protected float maxHealth = 50f;
    [SerializeField]  protected float moveSpeed = 12f;
    [SerializeField] protected float contactDamage = 30f;
    [SerializeField] protected bool shootTowardsPlayer;
    [SerializeField] protected bool moveTowardsPlayer;

    protected float health;
    private bool initialized = false;

    protected abstract void Shoot();
    protected abstract void Move();

    public void Die()
    {
        Destroy(this.gameObject);
    }

    public void Heal(float heal)
    {
        health += heal;
        if (health > maxHealth) 
            heal = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (!initialized)
        {
            health = maxHealth;
            initialized = true;
        }
        health -= damage;
        Debug.Log(health);
        if (health <= 0)
            Die();
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

        if (player != null)
        {
            player.TakeDamage(contactDamage);
        }
    }
}