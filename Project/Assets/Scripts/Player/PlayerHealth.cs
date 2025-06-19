using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float maxHealth;
    [SerializeField] private AudioClip[] onHitClips;

    private float health;

    private void Start()
    {
        health = maxHealth;
    }
    
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
        SoundManager.Instance.PlaySoundClipFromArray(onHitClips, transform.position);
        health -= damage;
        if (health < 0)
            Die();
    }
}
