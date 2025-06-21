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
    [SerializeField] protected GameObject onDestroyEffect;
    [SerializeField] protected AudioClip[] onDieClip;
    [SerializeField] protected AudioClip[] enemySpawnClips;
    [SerializeField] protected float shootTime, noShootTime, waitTillFirstShot;
    [SerializeField] protected bool stopBeforePlayer;
    [SerializeField] protected float maxDistToPlayer = 80f;
    [SerializeField] protected PCBHeal pcbHeal;
    
    public float enemyMissedDamage = 30f;

    protected bool movementAllow = true;
    protected bool firstShot = true;
    protected bool shootingAllowed = false;
    protected float timer;
    protected float health;
    private bool initialized = false;

    protected abstract void Shoot();
    protected abstract void Move();

    protected void HandleMoveAllow()
    {
        if (stopBeforePlayer && Vector3.Distance(transform.position, PlayerInputHandler.Instance.transform.position) < maxDistToPlayer)
        {
            movementAllow = false;
        }
    }

    protected void HandleShootAllow()
    {
        if (firstShot)
        {
            timer += Time.deltaTime;
            if (!shootingAllowed && timer > waitTillFirstShot)
            {
                timer = 0f;
                shootingAllowed = true;
                firstShot = false;
            }
            return;
        }
        timer += Time.deltaTime;
        if (!shootingAllowed && timer > noShootTime)
        {
            timer = 0f;
            shootingAllowed = true;
        }
        else if (shootingAllowed && timer > shootTime)
        {
            timer = 0f;
            shootingAllowed = false;
        }
    }

    public void Die(bool spawnHeal=true)
    {
        Debug.Log("Instantiate pcb heal");
        SoundManager.Instance.PlaySoundClipFromArray(onDieClip, transform.position);
        ParticleSystemManager.Instance.SpawnParticles(onDestroyEffect, transform.position, transform.rotation);
        GetComponentInParent<WaveScript>().OnEnemyDie(this);
        Instantiate(pcbHeal, transform.position, transform.rotation);
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
        if (health <= 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

        if (player != null)
        {
            player.TakeDamage(contactDamage);
            Die(spawnHeal:false);
        }
    }
}