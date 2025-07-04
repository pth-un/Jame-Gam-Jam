using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(float damage);

    public void Heal(float health);
    public void Die(bool spawnHeal=false);
}
