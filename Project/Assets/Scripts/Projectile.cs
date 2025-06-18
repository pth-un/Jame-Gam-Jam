using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float bulletProjectileForce;

    [Serializable]
    private enum BulletType{ bullet, rocket, laser}
    [SerializeField] private BulletType bulletType;

    private IEnumerator coroutine;
    private float sinCenterX;


    public void OnSpawn()
    {
        switch (bulletType)
        {
            case BulletType.bullet:
                GetComponent<Rigidbody>().AddForce(transform.forward * bulletProjectileForce, ForceMode.Impulse);
                int destroyWaitTime = 5;
                coroutine = WaitCoroutine(destroyWaitTime);
                StartCoroutine(coroutine);
                return;
            case BulletType.rocket:
                sinCenterX = transform.position.x;
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

        //Vector3 move = straightMovement;
//
        //float sin = Mathf.Sin(move.z * frequency) * amplitude;
        //move.x = sinCenterX + sin;
//
        //transform.position = move;
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
        }
    }
}
