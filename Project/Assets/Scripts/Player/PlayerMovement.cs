using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private float maxVelocity;

    private PlayerInputHandler playerInputHandler;
    private Rigidbody rb;

    private void Start()
    {
        playerInputHandler = GetComponent<PlayerInputHandler>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        float multiplier = 100f;
        Vector2 movementDelta = playerInputHandler.GetMovementDelta();
        Vector3 movementForce = new Vector3 (movementDelta.x, 0, movementDelta.y);
        movementForce = movementForce * playerSpeed * Time.deltaTime * multiplier;
       
        if (rb.linearVelocity.magnitude >= maxVelocity) movementForce = Vector3.zero;
        //rb.AddForce(movementForce, ForceMode.Force);

        rb.linearVelocity = movementForce;
    }
}
