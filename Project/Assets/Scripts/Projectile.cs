using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float bulletProjectileForce;
    [SerializeField] private GameObject shootParticles, onHitEffect;
    [SerializeField] private AudioClip[] onHitClips;

    [Serializable]
    private enum BulletType { bullet, rocket, laser }
    [SerializeField] private BulletType bulletType;

    private IEnumerator coroutine;


    public void OnSpawn()
    {
        switch (bulletType)
        {
            case BulletType.bullet:
                SpawnParticles();
                GetComponent<Rigidbody>().AddForce(transform.forward * bulletProjectileForce, ForceMode.Impulse);
                int destroyWaitTime = 10;
                coroutine = WaitCoroutine(destroyWaitTime);
                StartCoroutine(coroutine);
                return;
            case BulletType.rocket:
                return;
        }
    }

    private void Update()
    {
        if (bulletType == BulletType.rocket)
        {
            HandleRocketMovement();
        }
    }

    private void HandleRocketMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, PlayerInputHandler.Instance.transform.position, bulletProjectileForce * Time.deltaTime);
    }

    private IEnumerator WaitCoroutine(int _destroyWaitTime)
    {
        yield return new WaitForSeconds(_destroyWaitTime);
        GetComponentInParent<BulletsSpool>().ResetBullet(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakeDamage(damage);

            GetComponentInParent<BulletsSpool>().ResetBullet(this.gameObject);

            SpawnParticlesOnHit(collision);

            SoundManager.Instance.PlaySoundClipFromArray(onHitClips, collision.contacts[0].point);
        }
    }

    private void SpawnParticlesOnHit(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0];
        Vector3 location = contactPoint.point;
        Vector3 normal = contactPoint.normal;
        Quaternion rotation = Quaternion.LookRotation(normal);

        ParticleSystemManager.Instance.SpawnParticles(onHitEffect, location, rotation);
    }

    private void SpawnParticles()
    {
        ParticleSystemManager.Instance.SpawnParticles(shootParticles, transform.position, transform.rotation);
    }
}
