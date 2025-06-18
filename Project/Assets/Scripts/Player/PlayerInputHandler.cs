using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private static PlayerInputHandler instance;
    public static PlayerInputHandler Instance { get { return instance; }}

    private PlayerInputActions inputActions;
    private bool isFiring;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();

        if (instance != null && instance == this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        inputActions.Player.Fire.performed += OnPlayerFire;
        inputActions.Player.Fire.canceled += OnPlayerFire_Stop;
    }

    private void OnPlayerFire_Stop(InputAction.CallbackContext context)
    {
        isFiring = false;
    }

    private void OnPlayerFire(InputAction.CallbackContext context)
    {
        isFiring = true;
    }

    public Vector2 GetMovementDelta()
    {
        Vector2 movementDelta = inputActions.Player.Movement.ReadValue<Vector2>();
        return movementDelta;
    }

    public bool CheckFiring()
    {
        return isFiring;
    }
}
