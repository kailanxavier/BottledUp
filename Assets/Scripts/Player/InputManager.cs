using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput;

    // performed event checks
    public event System.Action DashPerformed;       // dash
    public event System.Action SlamPerformed;       // slam
    public event System.Action InteractPerformed;   // interact
    public event System.Action AttackPerformed;     // attack
    public event System.Action JumpPerformed;       // jump

    public event System.Action SprintStart;
    public event System.Action SprintEnd;

    public Vector2 InputVector { get { return _inputVector; } }
    private Vector2 _inputVector;

    private void Awake()
    {
        _playerInput = new PlayerInput();

        Setup();
    }

    void Setup()
    {
        // movement bind
        _playerInput.Player.Movement.performed += ctx => _inputVector = ctx.ReadValue<Vector2>();
        _playerInput.Player.Movement.canceled += ctx => _inputVector = Vector2.zero;

        // sprint bind
        _playerInput.Player.Sprint.started += ctx => OnStartSprint();
        _playerInput.Player.Sprint.canceled += ctx => OnEndSprint();

        // jump bind
        _playerInput.Player.Jump.performed += ctx =>
        {
            JumpPerformed?.Invoke();
        };
        
        // interact bind
        _playerInput.Player.Interact.performed += ctx =>
        {
            InteractPerformed?.Invoke();
        };

        // attack bind
        _playerInput.Player.Attack.performed += ctx =>
        {
            AttackPerformed?.Invoke();
        };

        // dash bind
        _playerInput.Player.Dash.performed += ctx =>
        {
            DashPerformed?.Invoke();
        }; 
        
        // slam bind
        _playerInput.Player.Slam.performed += ctx =>
        {
            SlamPerformed?.Invoke();
        };
    }

    void OnStartSprint() => SprintStart?.Invoke();
    void OnEndSprint() => SprintEnd?.Invoke();

    private void OnEnable()
    {
        _playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Player.Disable();
    }
}
