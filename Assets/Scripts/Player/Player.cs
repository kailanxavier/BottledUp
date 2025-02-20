using UnityEngine;

public class Player : MonoBehaviour
{
    private InputManager _inputManager;

    private Rigidbody playerRigidbody;

    [Header("Player controls: ")]
    [SerializeField] private float playerSpeed = 7.5f;
    [SerializeField] private float lookSpeed = 10f;
    [SerializeField] private float jumpForce = 4f;
    [SerializeField] private Transform lookDirection;

    [SerializeField] private float playerDrag;
    
    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
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
    }

    // Function to check if the player is grounded
    private bool IsGrounded()
    {
        bool isGrounded = false;
        float rayLength = 1.2f;

        isGrounded = Physics.Raycast(transform.position, Vector3.down, rayLength);

        return isGrounded;
    }

    private void MovePlayer()
    {

        Vector2 inputVector = _inputManager.GetInputVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        float speedMultiplier = 500f; // this is a hack and should probably be rewritten, just like the rest of codebase

        if (IsGrounded())
        {
            moveDir = lookDirection.forward * inputVector.y + lookDirection.right * inputVector.x;
            playerRigidbody.AddForce((playerSpeed * Time.deltaTime * moveDir) * speedMultiplier, ForceMode.Force);
            playerRigidbody.drag = playerDrag;
        }
        else
        {
            playerRigidbody.drag = 0;
        }

        // This prevents the player from getting stuck in corners and from spinning when the y rotation is unlocked
        if (inputVector.x <= 0 || inputVector.y <= 0) playerRigidbody.angularDrag = 100;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * lookSpeed);
    }

    private void HandleJump()
    {
        if (_inputManager.CheckForJump() && IsGrounded())
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
