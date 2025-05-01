using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2ToLevel3 : MonoBehaviour
{
    // player
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerPosLevel3;

    // camera
    [SerializeField] private Transform currentCameraPosition;
    [SerializeField] private Transform cameraPositionLevel3;

    // level control
    [SerializeField] private GameObject levelToActivate;
    [SerializeField] private float dampTime = 0.1f;

    private bool triggered = false;

    private void Update()
    {
        if (triggered)
        {
            currentCameraPosition.position = Vector3.Slerp
                (
                    currentCameraPosition.position,
                    cameraPositionLevel3.position,
                    dampTime
                );
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            // transition camera
            triggered = true;

            // change player position
            player.position = playerPosLevel3.position;

            // set level and trigger active
            levelToActivate.SetActive(true);

            Destroy(this.gameObject, 1f);
        }
    }
}
