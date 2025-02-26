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

    public bool CheckForAttack()
    {
        bool attackPerformed = _playerInput.Player.Attack.triggered;
        return attackPerformed;
    }

    public bool CheckForInteraction()
    {
        bool interactPerformed = _playerInput.Player.Interact.triggered;
        return interactPerformed;
    }

    public bool CheckForJump()
    {
        bool jumpPerformed = _playerInput.Player.Jump.triggered;
        return jumpPerformed;
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
