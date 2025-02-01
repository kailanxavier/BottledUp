using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    public Vector2 GetInputVectorNormalized()
    {
        Vector2 inputVector = new Vector2(0, 0);
        inputVector = _playerInput.Player.Movement.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        
        return inputVector;
    }

    public bool CheckForInteraction()
    {
        bool interactPerformed = false;
        if (_playerInput.Player.Interact.triggered)
            interactPerformed = true;

        return interactPerformed;
    }

    private void OnEnable()
    {
        _playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Player.Disable();
    }
}
