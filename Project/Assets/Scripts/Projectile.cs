using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private IEnumerator coroutine;
    private int timer;

    public void OnSpawn()
    {
        int destroyWaitTime = 5;
        coroutine = WaitCoroutine(destroyWaitTime);
        StartCoroutine(coroutine);
    }

    private IEnumerator WaitCoroutine(int _destroyWaitTime)
    {
        yield return new WaitForSeconds(_destroyWaitTime);
        GetComponentInParent<BulletsSpool>().ResetBullet(this.gameObject);
    }
}
