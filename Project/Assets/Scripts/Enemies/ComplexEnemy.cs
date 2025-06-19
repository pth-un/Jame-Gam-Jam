using UnityEngine;

public class ComplexEnemy : Enemy
{
    [Header("ComplexEnemySettings")]
    [SerializeField] private float minDistToPlayer = 30f;

    private enum State
    {
        MoveStraight,
        MoveTowardsPlayer
    }
    private State currentState;

    private void Start()
    {
        currentState = State.MoveStraight;
    }

    private void Update()
    {
        HandleShootAllow();
        switch (currentState)
        {
            case State.MoveStraight:
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
                if (Vector3.Distance(transform.position, PlayerInputHandler.Instance.transform.position) <= minDistToPlayer)
                {
                    currentState = State.MoveTowardsPlayer;
                }
                return;
            case State.MoveTowardsPlayer:
                Move();
                return;
        }
    }

    protected override void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, PlayerInputHandler.Instance.transform.position, moveSpeed * Time.deltaTime);
        if(shootingAllowed) Shoot();
    }

    protected override void Shoot()
    {
        GetComponent<Gun>().HandleFire(1 << gameObject.layer, shootTowardsPlayer);
    }
}
