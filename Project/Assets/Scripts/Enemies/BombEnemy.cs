using System.ComponentModel.Design;
using UnityEngine;

public class BombEnemy : Enemy
{
    [Header("BombEnemySettings")]
    [SerializeField] private float minDistToPlayer;
    [SerializeField] private float bombRadius;
    [SerializeField] private float damage;
    [SerializeField] private LayerMask playerMask;

    private void Start()
    {
        SoundManager.Instance.PlaySoundClipFromArray(enemySpawnClips, transform.position);
    }
    private void Update()
    {
        Move();
    }
    protected override void Move()
    {
        if (Vector3.Distance(transform.position, PlayerInputHandler.Instance.transform.position) > minDistToPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerInputHandler.Instance.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            Blast();
        }
    }

    private void Blast()
    {
        if (Physics.CheckSphere(transform.position, bombRadius, playerMask))
        {
            PlayerInputHandler.Instance.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
        Die();
    }

    protected override void Shoot()
    {
        return;
    }
}
