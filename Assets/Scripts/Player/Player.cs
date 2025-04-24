using Cinemachine.Utility;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private InputManager _inputManager;
    private Rigidbody _playerRb;
    public Transform lookDirection;
    public Shockwave shockwaveImpulse;

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
    [SerializeField] private bool _airborne = false;
    [SerializeField] private float _groundCheckRadius = 0.3f;
    [SerializeField] private Transform _groundCheckTransform;
    [SerializeField] private LayerMask _groundLayerMask;

    [Header("Slope check: ")]
    [SerializeField]  private RaycastHit slopeHit;
    [SerializeField] private float maxSlopeAngle = 45f;

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
    [SerializeField] private bool _slamRecharged = true;
    [SerializeField] private float _slamForce = 40f;

    [Header("Dash: ")]
    [SerializeField] private bool _dashRecharged = true;
    [SerializeField] private float _dashSpeed = 30f;

    public CustomCollision playerCollider;

    private Vector3 _moveDir;
    private Vector3 _slopeMoveDir;
    private Vector2 _inputVector;

    protected float _currentMaxMoveSpeed;
    protected float _currentMoveSpeedMultiplier; // lmao markiplier


    private void Awake()
    {
        Initialize();

        // sprint action subscribe
        _inputManager.SprintStart += StartSprinting;
        _inputManager.SprintEnd += StopSprinting;
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

    private void Update()
    {
        // check if we have landed
        HasPlayerLanded();
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
        if (_canMove)
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

            // apply final force
            ApplyFinalForce(moveSpeed);

            // face look direction
            transform.forward = Vector3.Slerp(transform.forward, _moveDir, Time.deltaTime * _turnSpeed);
        }
    }

    private void ApplyFinalForce(float moveSpeed)
    {
        // apply force to rigidbody if on slope
        if (OnSlope())
        {
            _playerRb.AddForce(moveSpeed * GetSlopeMoveDir(), ForceMode.Force);
        }
        // apply force if on flat ground
        else
        {
            _playerRb.AddForce(moveSpeed * _moveDir, ForceMode.Force);
        }
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, 5f, _groundLayerMask))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDir()
    {
        return Vector3.ProjectOnPlane(_moveDir, slopeHit.normal).normalized;
    }

    void OnJump()
    {
        if (_canJump && _isGrounded)
        {
            _isJumping = !_isGrounded;
            _playerRb.AddForce(new Vector3(0f, _jumpEnergy, 0f), ForceMode.Impulse);
        }
    }

    void OnDash()
    {
        // dash recharge time
        float rechargeTime = 2f;
        if (_canDash && CurrentSpeed > 0.05f)
        {
            if (_dashRecharged)
            {
                // apply force
                _playerRb.AddForce(_moveDir * _dashSpeed, ForceMode.Impulse);

                // set and reset
                _dashRecharged = false;
                Invoke(nameof(RechargeDash), rechargeTime);
            }
        }
    }

    void OnSlam()
    {
        // slam recharge time
        float rechargeTime = 2f;
        if (_canSlam && !_isGrounded)
        {
            if (_slamRecharged)
            {
                // apply force
                _playerRb.AddForce(new Vector3(0f, -_slamForce, 0f), ForceMode.Impulse);

                // set and reset
                _slamRecharged = false;
                Invoke(nameof(RechargeSlam), rechargeTime);
            }
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

    private void RechargeDash()
    {
        _dashRecharged = true;
    }

    private void RechargeSlam()
    {
        _slamRecharged = true;
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
        _inputManager.JumpPerformed += OnJump;

        // dash
        _inputManager.DashPerformed += OnDash;

        // slam
        _inputManager.SlamPerformed += OnSlam;
    }

    void Unsubscribe()
    {
        // jump
        _inputManager.JumpPerformed -= OnJump;

        // dash
        _inputManager.DashPerformed -= OnDash;

        // slam
        _inputManager.SlamPerformed -= OnSlam;
    }

    // 
    private void HasPlayerLanded()
    {
        if (_airborne && _isGrounded)
        {
            NotifyPlayerLanded();
        }

        _airborne = !_isGrounded;
    }

    public void NotifyPlayerLanded()
    {
        //Debug.Log("sir we have successfully landed");
        shockwaveImpulse.GoBoom();
    }
}
