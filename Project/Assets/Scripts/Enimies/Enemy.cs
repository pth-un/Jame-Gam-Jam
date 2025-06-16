using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected float maxHealth;
    [SerializeField]
    protected float health;
    [SerializeField]
    protected List<Gun> gunList;
    [SerializeField]
    protected float moveSpeed;
    [SerializeField]
    protected float contactDamage;
    protected abstract void Shoot();
    protected abstract void Move();

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

    private void OnCollisionEnter(Collision collision)
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

        if (player != null)
        {
            player.TakeDamage(contactDamage);
        }
    }
}
