using System.Collections;
using UnityEngine;

public class ZigZagEnemy : Enemy
{
    [Header("ZigZagEnemySettings")]
    [SerializeField] private float sidewaysMovementForce;
    [SerializeField] private float leftMovementTime, rightMovementTime;

    private enum Direction { left, right}
    private Direction direction;
    private bool coroutineStarted;

    private void Start()
    {
        SoundManager.Instance.PlaySoundClipFromArray(enemySpawnClips, transform.position);
        direction = new Direction();
        direction = Direction.left;
    }

    private void Update()
    {
        Shoot();
        HandleShootAllow();
        HandleMoveAllow();
        Move();
    }

    protected override void Shoot()
    {
        if(shootingAllowed) GetComponent<Gun>().HandleFire(1 << gameObject.layer, shootTowardsPlayer);
    }

    protected override void Move()
    {
        switch (direction)
        {
            case Direction.left:
                if (movementAllow)
                {
                    if (!coroutineStarted) StartCoroutine(WaitCoroutine(leftMovementTime));
                    transform.position += transform.right * sidewaysMovementForce * -1 * Time.deltaTime + transform.forward * moveSpeed * Time.deltaTime;
                }
                return;
            case Direction.right:
                if (movementAllow)
                {
                    if (!coroutineStarted) StartCoroutine(WaitCoroutine(rightMovementTime));
                    transform.position += transform.right * sidewaysMovementForce * Time.deltaTime + transform.forward * moveSpeed * Time.deltaTime;
                }
                return;
        }
    }

    private void ChangeDirection()
    {
        if (direction == Direction.left) direction = Direction.right;
        else direction = Direction.left;
    }

    private IEnumerator WaitCoroutine(float waitTime)
    {
        coroutineStarted = true;
        yield return new WaitForSeconds(waitTime);
        ChangeDirection();
        coroutineStarted = false;
    }
}
