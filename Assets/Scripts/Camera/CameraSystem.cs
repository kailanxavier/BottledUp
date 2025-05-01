using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine.Rendering;

public class CameraSystem : MonoBehaviour
{
    [Header("Move controls: ")]
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    private bool dragMoveEnabled;
    private Vector2 lastMousePos;
    private Vector3 originalPosition;
    [SerializeField] private float dragPanSpeed = 2.5f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Vector2 xLimits = new(-10f, 10f);
    [SerializeField] private Vector2 zLimits = new(-10f, 10f);

    [Header("Zoom controls: ")]
    [SerializeField] private float followOffsetMinY = 10f;
    [SerializeField] private float followOffsetMaxY = 50f;
    private Vector3 followOffset;


    [Header("Rotate controls: ")]
    [SerializeField] private float rotationSpeed = 600f;
    private bool dragRotateEnabled;

    public Transform playerPos;
    public Player player;



    private void Awake()
    {
        followOffset = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
    }

    private void Update()
    {
        DragPanMove();
        DragPanRotate();
        CameraZoom();

        // RIP Controller Support
        //ControllerPanMove();
        //ControllerPanRotate();
    }

    private void CameraZoom()
    {
        float mouseZoomAmount = 3f;

        if (Input.mouseScrollDelta.y < 0)
            followOffset.y += mouseZoomAmount;

        if (Input.mouseScrollDelta.y > 0)
            followOffset.y -= mouseZoomAmount;

        followOffset.y = Mathf.Clamp(followOffset.y, followOffsetMinY, followOffsetMaxY);

        float zoomSpeed = 3f;
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Vector3.Lerp(cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset, followOffset, Time.deltaTime * zoomSpeed);

    }

    //private void ControllerPanMove()
    //{
    //    float moveSpeed = 10f;

    //    Vector2 inputVector = new(Input.GetAxis("RightStickHorizontal"), Input.GetAxis("RightStickVertical"));
    //    inputVector = inputVector.normalized;

    //    var inputDir = Vector3.zero;

    //    inputDir.x = inputVector.x;
    //    inputDir.z = -inputVector.y;

    //    Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

    //    transform.position += moveSpeed * Time.deltaTime * moveDir;
    //}
    //private void ControllerPanRotate()
    //{
    //    Vector2 inputVector = new(Input.GetAxisRaw("RightTrigger"), Input.GetAxisRaw("LeftTrigger"));
    //    float rotateSpeed = 0.2f;

    //    if (inputVector.x > 0)
    //    {
    //        transform.Rotate(Vector3.up, -rotateSpeed, Space.Self);
    //    }
    //    if (inputVector.y > 0)
    //    { 
    //        transform.Rotate(Vector3.up, rotateSpeed, Space.Self);
    //    }
    //}

    private void DragPanMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragMoveEnabled = true;
            lastMousePos = Input.mousePosition;
            originalPosition = transform.position; // keep track of original position
        }

        if (Input.GetMouseButtonUp(0))
        {
            dragMoveEnabled = false;
        }

        if (dragMoveEnabled)
        {
            Vector2 mouseMoveDelta = (Vector2)Input.mousePosition - lastMousePos;
            Vector3 inputDir = new Vector3(
                -mouseMoveDelta.x * dragPanSpeed,
                0f,
                -mouseMoveDelta.y * dragPanSpeed
            );
            Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;
            Vector3 newPosition = transform.position + moveSpeed * Time.deltaTime * moveDir;

            // apply position limits
            newPosition.x = Mathf.Clamp(newPosition.x, xLimits.x, xLimits.y);
            newPosition.z = Mathf.Clamp(newPosition.z, zLimits.x, zLimits.y);

            transform.position = newPosition;

            lastMousePos = Input.mousePosition; // save last mouse position
        }
    }

    private void DragPanRotate()
    {
        int rightClickButton = 1;

        if (Input.GetMouseButtonDown(rightClickButton))
        {
            dragRotateEnabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (Input.GetMouseButtonUp(rightClickButton))
        {
            dragRotateEnabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (dragRotateEnabled && !dragMoveEnabled)
        {
            float horizontalInput = Input.GetAxisRaw("Mouse X") * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, horizontalInput, Space.Self);
        }
    }
}
