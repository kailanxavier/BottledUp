using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine.Rendering;

public class CameraSystem : MonoBehaviour
{
    // Mouse move camera controls
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private bool dragMoveEnabled;
    private Vector2 lastMousePos;

    // Mouse zoom camera controls
    private Vector3 followOffset;
    private float followOffsetMinY = 10f;
    private float followOffsetMaxY = 100f;

    // Mouse rotate camera controls
    private bool dragRotateEnabled;
    private float rotationSpeed = 600f;

    private void Awake()
    {
        followOffset = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
    }

    private void Update()
    {
        DragPanMove();
        DragPanRotate();
        CameraZoom();
    }

    private void CameraZoom()
    {
        float zoomAmount = 3f;
        if (Input.mouseScrollDelta.y < 0)
        {
            followOffset.y += zoomAmount;
        }
        if (Input.mouseScrollDelta.y > 0)
        {
            followOffset.y -= zoomAmount;
        }

        followOffset.y = Mathf.Clamp(followOffset.y, followOffsetMinY, followOffsetMaxY);

        float zoomSpeed = 5f;
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
            Vector3.Lerp(cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset, followOffset, Time.deltaTime * zoomSpeed);
    }

    private void DragPanMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragMoveEnabled = true;
            lastMousePos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            dragMoveEnabled = false;
        }

        if (dragMoveEnabled)
        {
            Vector2 mouseMoveDelta = (Vector2)Input.mousePosition - lastMousePos;
            Vector3 inputDir = new Vector3(0, 0, 0);

            float dragPanSpeed = 2.5f;
            inputDir.x = -mouseMoveDelta.x * dragPanSpeed;
            inputDir.z = -mouseMoveDelta.y * dragPanSpeed;

            Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

            float moveSpeed = 5f;
            transform.position += Time.deltaTime * moveDir * moveSpeed;

            lastMousePos = Input.mousePosition;
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
