using UnityEngine;

public class AdvancedSkyboxController : MonoBehaviour
{
    [Header("----------Rotation Settings----------")]
    public bool enableRotation = true;
    public float rotationSpeed = 1f;

    [Header("----------Rotation Constraints----------")]
    public bool constrainRotation = true;
    public float minRotation = -180f;
    public float maxRotation = 180f;

    [Header("----------Player Follow----------")]
    public Transform player;
    public float followSpeed = 0.5f;
    public bool constrainPlayerFollow = true;

    private Material skyboxMaterial;
    private float currentRotation = 0f;
    private Vector3 lastPlayerPosition;

    void Start()
    {
        skyboxMaterial = RenderSettings.skybox;

        if (player != null)
        {
            lastPlayerPosition = player.position;
        }

    }

    void Update()
    {
        if (skyboxMaterial == null) return;

        if (enableRotation)
        {
            float rotationDelta = rotationSpeed * Time.deltaTime;

            if (player != null)
            {
                Vector3 playerDelta = player.position - lastPlayerPosition;
                float playerRotationDelta = playerDelta.x * followSpeed;

                if (constrainPlayerFollow)
                {
                    float potentialRotation = currentRotation + playerRotationDelta;
                    if (constrainRotation)
                    {
                        potentialRotation = Mathf.Clamp(potentialRotation, minRotation, maxRotation);
                        playerRotationDelta = potentialRotation - currentRotation;
                    }
                }

                rotationDelta += playerRotationDelta;
                lastPlayerPosition = player.position;
            }

            float newRotation = currentRotation + rotationDelta;

            if (constrainRotation)
            {
                newRotation = Mathf.Clamp(newRotation, minRotation, maxRotation);

                if ((newRotation >= maxRotation && rotationSpeed > 0) || (newRotation <= minRotation && rotationSpeed < 0))
                {
                    rotationSpeed = 0f; 
                }
            }
            else
            {
                if (newRotation >= 360f) newRotation -= 360f;
                if (newRotation < 0f) newRotation += 360f;
            }

            currentRotation = newRotation;
            skyboxMaterial.SetFloat("_Rotation", currentRotation);
        }

    }
    //my funcitons
    public void SetRotationSpeed(float speed)
    {
        rotationSpeed = speed;
    }

    public void StopRotation()
    {
        enableRotation = false;
    }

    public void StartRotation()
    {
        enableRotation = true;
    }

    public void SetRotationConstraints(float min, float max)
    {
        minRotation = min;
        maxRotation = max;
        constrainRotation = true;

        currentRotation = Mathf.Clamp(currentRotation, minRotation, maxRotation);
    }

    public void DisableConstraints()
    {
        constrainRotation = false;
    }

    public void ResetRotation()
    {
        currentRotation = 0f;
        if (skyboxMaterial != null)
        {
            skyboxMaterial.SetFloat("_Rotation", currentRotation);
        }
    }
}