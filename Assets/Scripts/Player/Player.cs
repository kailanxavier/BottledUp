using Cinemachine.Utility;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private InputManager _inputManager;
    private Rigidbody _playerRb;
    public Transform lookDirection;

    public bool IsGrounded { get { return _isGrounded; } }
    public bool IsSprinting { get { return _isSprinting; } }
    public bool IsJumping { get { return _isJumping; } }
    public float CurrentSpeed { get { return _playerRb.velocity.magnitude; } }

    [Header("Flags: ")]
    [SerializeField] private bool _shouldGroundCheck = true;
    [SerializeField] private bool _canMove = true;
    [SerializeField] private bool _canJump = true;
    [SerializeField] private bool _canSprint = true;
    [SerializeField] private bool _canSlam = true;
    [SerializeField] private bool _canDash = true;

    [Header("Ground check: ")]
    [SerializeField] private bool _isGrounded = true;
    [SerializeField] private float _groundCheckRadius = 0.3f;
    [SerializeField] private Transform _groundCheckTransform;
    [SerializeField] private LayerMask _groundLayerMask;

    [Header("Movement: ")]
    [SerializeField] private float _baseMoveSpeed = 15.0f;
    [SerializeField] private float _baseMoveSpeedMultiplier = 100.0f;
    [SerializeField, Tooltip("Values between 0.7 and 0.9 work the best due to the player not coming to a complete hault.")] 
    private float _stopSlideSpeed = 1f;
    [SerializeField] private float _turnSpeed = 10f;

    [Header("Jumping: ")]
    [SerializeField] private float _jumpEnergy;
    [SerializeField] private bool _isJumping;

    [Header("Sprinting: ")]
    [SerializeField] private bool _isSprinting = false;
    [SerializeField] private float _sprintMoveSpeed = 20f;
    [SerializeField] private float _sprintMoveSpeedMultiplier = 150f;

    [Header("Slam: ")]
    [SerializeField] private float _slamForce = 40f;

    [Header("Dash: ")]
    [SerializeField] private float _dashSpeed = 30f;

    public CustomCollision playerCollider;

    private Vector3 _moveDir;
    private Vector2 _inputVector;

    protected float _currentMaxMoveSpeed;
    protected float _currentMoveSpeedMultiplier; // lmao markiplier


    private void Awake()
    {
        Initialize();


        _inputManager.SprintStart += StartSprinting;
        _inputManager.SprintEnd += StopSprinting;

        _inputManager.JumpPerformed += Jump;
    }

    private void StartSprinting()
    {
        if (_canSprint && _isGrounded)
        {
            _isSprinting = true;
            _currentMaxMoveSpeed = _sprintMoveSpeed;
            _currentMoveSpeedMultiplier = _sprintMoveSpeedMultiplier;
        }
    }

    private void StopSprinting()
    {
        if (_isSprinting)
        {
            _isSprinting = false;
            ResetBaseSpeed();
        }
    }

    private void Initialize()
    {
        _inputManager = GetComponent<InputManager>();
        _playerRb = GetComponent<Rigidbody>();

        ResetBaseSpeed();
    }

    private void ResetBaseSpeed()
    {
        _currentMaxMoveSpeed = _baseMoveSpeed;
        _currentMoveSpeedMultiplier = _baseMoveSpeedMultiplier;
    }

    private void FixedUpdate()
    {
        // move if grounded
        if (_isGrounded)
            Move();

        // ground check, etc...
        PhysicsChecks();
    }

    private void Move()
    {
        _inputVector = _inputManager.InputVector; // get player move input

        //Debug.Log(_inputVector);

        // if we act like a penguin on ice skates stop the player
        if (_playerRb.velocity.magnitude <= _stopSlideSpeed && _inputVector == Vector2.zero)
            _playerRb.velocity = Vector3.zero;

        // limit speed
        float ratioRemainder = (_currentMaxMoveSpeed - _playerRb.velocity.magnitude) / _currentMaxMoveSpeed;
        _moveDir = (lookDirection.forward * _inputVector.y) + (lookDirection.right * _inputVector.x);
        float moveSpeed = ratioRemainder * _currentMoveSpeedMultiplier;

        // apply force to rigidbody
        _playerRb.AddForce(moveSpeed * _moveDir, ForceMode.Force);

        // face look direction
        transform.forward = Vector3.Slerp(transform.forward, _moveDir, Time.deltaTime * _turnSpeed);
    }

    void Jump()
    {
        if (_canJump && _isGrounded)
        {
            _isJumping = !_isGrounded;
            _playerRb.AddForce(new Vector3(0f, _jumpEnergy, 0f), ForceMode.Impulse);
        }
    }

    void PhysicsChecks()
    {
        // check for ground
        if (_shouldGroundCheck)
            GroundCheck();
    }

    void GroundCheck()
    {
        _isGrounded = Physics.CheckSphere(_groundCheckTransform.position, _groundCheckRadius, _groundLayerMask);
    }

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    void Subscribe()
    {
        // jump
        _inputManager.JumpPerformed += Jump;

        // dash

        // slam
    }

    void Unsubscribe()
    {
        // jump
        _inputManager.JumpPerformed -= Jump;

        // dash

        // slam
    }
}
