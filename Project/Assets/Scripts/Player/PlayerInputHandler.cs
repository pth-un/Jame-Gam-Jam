using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private static PlayerInputHandler instance;
    public static PlayerInputHandler Instance { get { return instance; }}

    public event EventHandler OnReloadEvent;

    private PlayerInputActions inputActions;
    private bool isFiring;

    private void Awake()
    {

        if (instance != null && instance == this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            inputActions = new PlayerInputActions();
            inputActions.Player.Enable();
            instance = this;

            inputActions.Player.Reload.performed += OnReload;
        }
    }

    private void Start()
    {
        inputActions.Player.Fire.performed += OnPlayerFire;
        inputActions.Player.Fire.canceled += OnPlayerFire_Stop;

    }

    private void OnReload(InputAction.CallbackContext context)
    {
        OnReloadEvent?.Invoke(this, EventArgs.Empty);
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
