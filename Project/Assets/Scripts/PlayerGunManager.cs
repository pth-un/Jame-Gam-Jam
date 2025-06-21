using UnityEngine;

public class PlayerGunManager : MonoBehaviour
{
    [SerializeField] private Gun[] playerGuns;

    private Gun currentGun;

    private void Start()
    {
        currentGun = playerGuns[0];
    }
}
