using UnityEngine;

public class Player : MonoBehaviour
{
    private InputManager _inputManager;
    private CharacterController _characterController;

    private Vector3 _lastInteractionDirection;
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float lookSpeed = 10f;
    
    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Move
        MovePlayer();
        
        // Jump
        
        // Sprint?
        
        // Interact
        HandleInteractions();
    }

    private void MovePlayer()
    {
        Vector2 inputVector = _inputManager.GetInputVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        float playerHeight = 2f; // Player height for CapsuleCast
        float playerRadius = 0.7f;
        float moveDistance = playerSpeed * Time.deltaTime;
        
        // CapsuleCast to check if the player can move.
        bool canMove = !Physics.CapsuleCast(transform.position, 
            transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        // Might need to refactor this soon because it looks like voodoo
        // and the if/elses are lowkey brain fart inducing.
        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;     // Normalized is here to make sure the 
                                                                            // player moves at the same speed even
                                                                            // if inputVector < 1.
            canMove = !Physics.CapsuleCast(transform.position, 
                transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;     // Normalized is here to make sure the 
                                                                                // player moves at the same speed even
                                                                                // if inputVector < 1.
                canMove = !Physics.CapsuleCast(transform.position, 
                    transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else {} // Do nothing because the player cannot move in any directions.
            }
        }
        
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * lookSpeed);
    }
    
    private void HandleInteractions()
    {
        Vector2 inputVector = _inputManager.GetInputVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        // Keep track of the last direction the player looked at to handle interactions.
        if (moveDir != Vector3.zero) _lastInteractionDirection = moveDir;

        float interactionDistance = 2.0f;

        if (Physics.Raycast(transform.position, _lastInteractionDirection, out RaycastHit hit, interactionDistance))
        {
            Debug.Log(hit.transform);
        }
        else
        {
            Debug.Log("No interaction");
        }
    }
}
