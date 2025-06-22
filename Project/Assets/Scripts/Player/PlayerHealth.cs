using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour, IDamageable
{
    public static PlayerHealth Instance;


    [SerializeField]
    private float maxHealth, enemyKillHealthIncrease;
    [SerializeField] private AudioClip[] onHitClips;

    private float health;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        health = maxHealth;
    }

    public void Die(bool spawnHeal = false)
    {
        Debug.Log("Dead");
        SceneManager.LoadScene("MainMenu");
    }

    public void Heal(float heal)
    {
        float currentHealth = health + heal;
        if (currentHealth > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health = currentHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        SoundManager.Instance.PlaySoundClipFromArray(onHitClips, transform.position);
        health -= damage;
        if (health < 0)
            Die();
    }

    public float GetPlayerHealthRate()
    {
        return health / maxHealth;
    }

    public void BoostMaxHealth(float boostStatValue)
    {
        maxHealth += boostStatValue;
    }

    public void ResetPlayerHealth()
    {
        health = maxHealth;
    }
}
