using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float bulletsFireRate;
    [SerializeField] private Transform[] gunShootPositions;
    [SerializeField] private AudioClip[] gunShootAudionClips;
    [SerializeField] private bool isPlayerGun;


    private BulletsSpool bulletsSpool;
    private PlayerInputHandler playerInputHandler;
    private float shootTimer;
    private bool canShoot;
    private float fireRate;

    private void Start()
    {
        fireRate = 60 / bulletsFireRate;
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
                    PlayShootSound(gunShootPos);
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
                    PlayShootSound(gunShootPos);
                }
                canShoot = false;
            }
        }
    }

    private void PlayShootSound(Transform soundPosition)
    {
        SoundManager.Instance.PlaySoundClipFromArray(gunShootAudionClips, soundPosition.position);
    }


    private void CheckShoot()
    {
        if (!canShoot)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= fireRate)
            {
                canShoot = true;
                shootTimer = 0f;
            }
        }
    }
}
