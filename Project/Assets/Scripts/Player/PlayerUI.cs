using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image healthBar;

    private void Update()
    {
        healthBar.fillAmount = GetComponent<PlayerHealth>().GetPlayerHealthRate();
    }
}
