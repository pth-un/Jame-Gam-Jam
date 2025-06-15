using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;

    private PlayerInputHandler playerInputHandler;

    private void Start()
    {
        playerInputHandler = GetComponent<PlayerInputHandler>();
    }
    private void Update()
    {
        float movementDelta = playerInputHandler.GetMovementDelta();
        Vector3 movement = Vector3.right * movementDelta * playerSpeed * Time.deltaTime;
        transform.position += movement;
    }
}
