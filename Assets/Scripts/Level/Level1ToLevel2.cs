using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level1ToLevel2 : MonoBehaviour
{
    // player
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerPosLevel2;

    // camera
    [SerializeField] private Transform currentCameraPosition;
    [SerializeField] private Transform cameraPositionLevel2;

    // level control
    [SerializeField] private GameObject levelToActivate;
    [SerializeField] private float dampTime = 1f;
    [SerializeField] private GameObject level2ToLevel3Trigger;

    private bool triggered = false;

    private void Update()
    {
        if (triggered)
        {
            currentCameraPosition.position = Vector3.Slerp
                (
                    currentCameraPosition.position,
                    cameraPositionLevel2.position,
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
            player.position = playerPosLevel2.position;

            // set level and trigger active
            levelToActivate.SetActive(true);
            level2ToLevel3Trigger.SetActive(true);

            Destroy(this.gameObject, 1f);
        }
    }

}
