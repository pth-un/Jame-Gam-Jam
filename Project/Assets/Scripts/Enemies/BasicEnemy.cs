using System.Collections;
using UnityEngine;

public class BasicEnemy : Enemy
{
    [SerializeField] private float moveTime;
    [SerializeField] private float shootTime;
    [SerializeField] private float xRandom, zRandom;

    private enum State
    {
        Moving,
        Shooting,
    }
    private State state;
    private float stateTimer;
    private Vector3 movePoint, currentPos;

    private void Start()
    {
        state = new State();
        state = State.Moving;
    }

    private void Update()
    {
        HandleLogic();
    }

    private void HandleLogic()
    {
        switch (state)
        {
            case State.Moving:
                stateTimer -= Time.deltaTime;
                Move();
                if (stateTimer <= 0)
                {
                    state = State.Shooting;
                    stateTimer = shootTime;
                }
                return;
            case State.Shooting:
                stateTimer -= Time.deltaTime;
                Shoot();
                if (stateTimer <= 0)
                {
                    state = State.Moving;


                    movePoint = transform.position;
                    stateTimer = moveTime;
                }
                return;
        }
    }

    protected override void Shoot()
    {

        GetComponent<Gun>().HandleFire();
    }

    protected override void Move()
    {
        if (movePoint == transform.position)
        {
            if (currentPos != transform.position)
            {
                currentPos = transform.position;
            }
            float moveX = transform.position.x + Random.Range(-xRandom, xRandom);
            float moveZ = transform.position.z + Random.Range(-zRandom, zRandom);

            movePoint = new Vector3(moveX, transform.position.y, moveZ);
            Debug.Log(movePoint);
        }
        transform.position = Vector3.MoveTowards(transform.position, movePoint, moveSpeed * Time.deltaTime);
    }
}

