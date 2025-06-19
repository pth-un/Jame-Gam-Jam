using System.Collections;
using UnityEngine;

public class BasicEnemy : Enemy
{
    [SerializeField] private Material material;
    [SerializeField] private float multiplier = 10f;

    private float intensity = 0.8f;
    
    private void Update()
    {
        HandleShootAllow();
        Move();
    }

    protected override void Shoot()
    {
        GetComponent<Gun>().HandleFire(1 << gameObject.layer, shootTowardsPlayer);
    }

    protected override void Move()
    {
        if (moveTowardsPlayer) transform.position = Vector3.MoveTowards(transform.position, PlayerInputHandler.Instance.transform.position, moveSpeed * Time.deltaTime);
        else transform.position += transform.forward * moveSpeed * Time.deltaTime;
        if(shootingAllowed) Shoot();
    }
}

