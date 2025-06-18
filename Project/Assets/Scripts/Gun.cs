using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float bulletsFireRate;
    [SerializeField] private Transform[] gunShootPositions;
    //[SerializeField] private AudioClip 
    [SerializeField] private bool isPlayerGun;


    private BulletsSpool bulletsSpool;
    private PlayerInputHandler playerInputHandler;
    private float shootTimer;
    private bool canShoot;

    private void Start()
    {
        if (isPlayerGun) playerInputHandler = PlayerInputHandler.Instance;
        bulletsSpool = BulletsSpool.Instance;
    }

    private void Update()
    {
        if (isPlayerGun) HandleFire(1 << gameObject.layer);
    }

    public void HandleFire(LayerMask layerMask, bool towardsPlayer=false)
    {
        if (!bulletsSpool) return;
        CheckShoot();
        if (isPlayerGun)
        {
            if (canShoot && playerInputHandler.CheckFiring())
            {
                foreach (Transform gunShootPos in gunShootPositions)
                {
                    bulletsSpool.OnShoot(gunShootPos, layerMask);
                }
                canShoot = false;
            }
        }
        else
        {
            if (canShoot)
            {
                foreach (Transform gunShootPos in gunShootPositions)
                {
                    bulletsSpool.OnShoot(gunShootPos, layerMask, towardsPlayer);
                }
                canShoot = false;
            }
        }
    }


    private void CheckShoot()
    {
        if (!canShoot)
        {
            shootTimer += Time.deltaTime;
            float fireRate = 60 / bulletsFireRate;
            if (shootTimer >= fireRate)
            {
                canShoot = true;
                shootTimer = 0f;
            }
        }
    }
}
