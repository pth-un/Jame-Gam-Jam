using UnityEngine;

public class EnemyDestroyer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.transform.GetComponent<Enemy>();
        if (enemy != null)
        {
            PlayerHealth.Instance.TakeDamage(enemy.enemyMissedDamage);
            enemy.Die(spawnHeal: false);
            GameManager.Instance.EnemyMissed();
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
