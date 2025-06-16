using UnityEngine;

public interface IDamageable 
{
    public void TakeDamage(float damage) { }
    
    public void Heal(float damage) { }
    public void Die() { }
}
