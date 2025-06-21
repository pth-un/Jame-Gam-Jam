using UnityEngine;

public class PCBHeal : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float heal;
    
    private void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
        if (playerHealth)
        {
            playerHealth.Heal(heal);
            Destroy(this.gameObject);
        }
    }
}
