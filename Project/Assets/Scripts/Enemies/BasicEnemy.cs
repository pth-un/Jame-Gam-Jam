using System.Collections;
using UnityEngine;

public class BasicEnemy : Enemy
{

    private void Update()
    {
        Move();
    }

    protected override void Shoot()
    {
        GetComponent<Gun>().HandleFire(shootTowardsPlayer);
    }

    protected override void Move()
    {
        if (moveTowardsPlayer) transform.position = Vector3.MoveTowards(transform.position, PlayerInputHandler.Instance.transform.position, moveSpeed * Time.deltaTime);
        else transform.position += transform.forward * moveSpeed * Time.deltaTime;
        Shoot();
    }
}

