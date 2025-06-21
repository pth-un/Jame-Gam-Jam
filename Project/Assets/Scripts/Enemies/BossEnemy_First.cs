using UnityEngine;

public class BossEnemy_First : Enemy
{
    private Vector3 randPos;
    private float startPosY, startPosZ;

    private void Start()
    {
        SoundManager.Instance.PlaySoundClipFromArray(enemySpawnClips, transform.position);
        startPosY = transform.position.y;
        startPosZ = transform.position.z;
        SetRandomPos();
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
        if (shootingAllowed) Shoot();
        else
        {
            if (transform.position == randPos) SetRandomPos();
            transform.position = Vector3.MoveTowards(transform.position, randPos, moveSpeed * Time.deltaTime);
        }
    }

    private void SetRandomPos()
    {
        float randX = Random.Range(-70, 70);
        randPos = new Vector3(randX, startPosY, startPosZ);
    }

    private void OnDestroy()
    {
        GameManager.Instance.BossEnemyDestroyed();
    }
}
