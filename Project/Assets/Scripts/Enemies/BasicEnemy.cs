using System.Collections;
using UnityEngine;

public class BasicEnemy : Enemy
{
    private void Start()
    {
        SoundManager.Instance.PlaySoundClipFromArray(enemySpawnClips, transform.position);
    }
    private void Update()
    {
        HandleShootAllow();
        HandleMoveAllow();
        Move();
    }

    protected override void Shoot()
    {
        GetComponent<Gun>().HandleFire(1 << gameObject.layer, shootTowardsPlayer);
    }

    protected override void Move()
    {
        if (movementAllow)
        {
            if (moveTowardsPlayer) transform.position = Vector3.MoveTowards(transform.position, PlayerInputHandler.Instance.transform.position, moveSpeed * Time.deltaTime);
            else transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        if (shootingAllowed) Shoot();
    }
}

