using System.Collections;
using UnityEngine;

public class DestroyParticleSytem : MonoBehaviour
{
    [SerializeField] private float destroyAfter=3f;

    private void Start()
    {
        StartCoroutine(WaitCoroutine(destroyAfter));
    }

    private IEnumerator WaitCoroutine(float waitTimer) {
        yield return new WaitForSeconds(waitTimer);
        Destroy(this.gameObject);
    }
}
