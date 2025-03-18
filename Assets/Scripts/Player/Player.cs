using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private InputManager _inputManager;
    private AnimationManager _animationManager;
    private Rigidbody playerRigidbody;

    public CustomCollision playerCollider;

    [Header("Player controls: ")]
    public float playerSpeed = 7.5f;
    public float lookSpeed = 5f;
    public float jumpForce = 4f;
    public float playerDrag;
    public Transform lookDirection;
    public bool isGrounded = true;

    [Header("Jump controls: ")]
    public bool canJump = true;

    // Note: Anything below 1.4f will
    // cause a super jump
    public float cooldownJump = 1.5f;

    [Header("Move controls: ")]
    public float cooldownMove = 1f;
    public bool canMove = true;

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _animationManager = GetComponent<AnimationManager>();
        playerRigidbody = GetComponent<Rigidbody>();

        playerCollider.EnterTriggerZone += OnPlayerTriggerEntered;
        playerCollider.ExitTriggerZone += OnPlayerTriggerExited;
    }

    private void FixedUpdate()
    {
        // Move
        MovePlayer();
    }

    private void Update()
    {
        // Jump
        HandleJump();
        _animationManager.HandleJumpAnimation();
    }

    private void MovePlayer()
    {
        Vector2 inputVector = _inputManager.GetInputVectorNormalized();
        Vector3 moveDir = new(inputVector.x, 0, inputVector.y);

        float groundSpeedMultiplier = 500f;
        float airSpeedMultiplier = 40f; // Speed multiplier to make air movement less responsive
        float maxAirSpeed = 5f;

        // IsGrounded check for ground and air movement
        if (canMove && isGrounded)
        {
            moveDir = lookDirection.forward * inputVector.y + lookDirection.right * inputVector.x;
            playerRigidbody.AddForce(groundSpeedMultiplier * playerSpeed * Time.deltaTime * moveDir, ForceMode.Acceleration);
            playerRigidbody.drag = playerDrag;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * lookSpeed);
        }
        else if (!isGrounded)
        {
            moveDir = lookDirection.forward * inputVector.y + lookDirection.right * inputVector.x;
            playerRigidbody.AddForce(airSpeedMultiplier * playerSpeed * Time.deltaTime * moveDir, ForceMode.Force);
            playerRigidbody.drag = 0f;

            // Calculate player speed
            Vector3 speedControl = new(playerRigidbody.velocity.x, 0f, playerRigidbody.velocity.z);

            // If magnitude > set maxAirSpeed set it to values of maxAirSpeed
            if (speedControl.magnitude > maxAirSpeed)
            {
                Vector3 newSpeed = speedControl.normalized * maxAirSpeed;
                playerRigidbody.velocity = new(newSpeed.x, playerRigidbody.velocity.y, newSpeed.z);
            }

            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * lookSpeed);
        }
        else
        {
            playerRigidbody.drag = 100f;
        }

        // This prevents the player from getting stuck in corners and from spinning when the y rotation is unlocked
        if (inputVector.x <= 0 || inputVector.y <= 0) playerRigidbody.angularDrag = 100;

    }
    void ResetMove()
    {
        canMove = true;
    }

    private void HandleJump()
    {
        if (_inputManager.CheckForJump() && isGrounded && canJump)
        {
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, jumpForce, playerRigidbody.velocity.z);
            canJump = false;
            Invoke(nameof(ResetJump), cooldownJump);
        }
    }

    private void ResetJump()
    {
        canJump = true;
    }

    private void OnPlayerTriggerEntered(Collider collider)
    {
        if (!isGrounded)
        {
            isGrounded = true;
            canMove = false;
            Invoke(nameof(ResetMove), cooldownMove);
        }
    }

    private void OnPlayerTriggerExited(Collider collider)
    {
        isGrounded = false;
    }
}
