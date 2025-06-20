using UnityEngine;
using UnityEngine.UI;

public class AnimateBulletIcon : MonoBehaviour
{
    [SerializeField] private Image bulletImg;

    private float rotationDuration; // Time in seconds to rotate 360°
    private float elapsedTime = 0f;
    private bool rotate = false;

    public void Rotate(float reloadDuration)
    {
        Debug.Log("HEre");
        rotationDuration = reloadDuration;
        rotate = true;
    }

    void Update()
    {
        if (!rotate) return;

        elapsedTime += Time.deltaTime;

        float progress = Mathf.Clamp01(elapsedTime / rotationDuration);
        float angle = 360f * progress;

        bulletImg.transform.rotation = Quaternion.Euler(0, 0, angle);

        if (progress >= 1f)
        {
            elapsedTime = 0f;
            rotate = false;
        }
    }
}
