using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputService : MonoBehaviour
{
    private InputSystem _inputSystem;

    public event Action Pressed;
    public event Action Paused;
    public event Action Continued;
    public event Action Restarted;

    public Vector2 PointerPosition { get; private set; }

    private void Awake()
    {
        _inputSystem = new InputSystem();
        _inputSystem.Game.Enable();
    }

    private void OnEnable()
    {
        _inputSystem.Game.Press.performed += OnPerformedPress;
        _inputSystem.Game.Restart.performed += OnPerformedRestart;
        _inputSystem.Game.PointerPosition.performed += OnPerformedPointerPosition;
    }

    private void OnDisable()
    {
        _inputSystem.Game.Press.performed -= OnPerformedPress;
        _inputSystem.Game.Restart.performed -= OnPerformedRestart;
        _inputSystem.Game.PointerPosition.performed -= OnPerformedPointerPosition;
    }

    private void OnPerformedPress(InputAction.CallbackContext context)
    {
        Pressed?.Invoke();
    }

    private void OnPerformedRestart(InputAction.CallbackContext context)
    {
        Restarted?.Invoke();
    }

    private void OnPerformedPointerPosition(InputAction.CallbackContext context)
    {
        PointerPosition = context.ReadValue<Vector2>();
    }
}