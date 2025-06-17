using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private BulletProjectileSO bulletProjectileSO;

    private IEnumerator coroutine;

    public void OnSpawn()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * bulletProjectileSO.bulletProjectileForce, ForceMode.Impulse);
        int destroyWaitTime = 5;
        coroutine = WaitCoroutine(destroyWaitTime);
        StartCoroutine(coroutine);
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
