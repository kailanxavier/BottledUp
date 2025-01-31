using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Player _player;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Player.Enable();
    }

    public Vector2 GetInputVectorNormalized()
    {
        Vector2 inputVector = new Vector2(0, 0);
        inputVector = _playerInput.Player.Movement.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        
        return inputVector;
    }

    private void Update()
    {
        // MovePlayer();
    }

    private void OnEnable()
    {
        _playerInput.Player.Enable();
    }
}
