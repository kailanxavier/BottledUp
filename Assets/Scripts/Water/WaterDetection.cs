using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class WaterDetection : MonoBehaviour
{
    // flags
    public bool IsSubmerged { get { return _isSubmerged; } }

    [Header("Water")]
    [SerializeField] private Collider waterCollider;
    [SerializeField] private string waterTag = "Water";
    [SerializeField] private float maxTimeSubmerged = 15f;
    [SerializeField] private float waterDrag = 5f;
    private bool _isSubmerged = false;

    [Header("Player")]
    [SerializeField] private CustomCollision playerCustomCollider;
    [SerializeField] private GameObject player;
    private Rigidbody _playerRigidbody;

    [Header("UI")]
    [SerializeField] private GameObject deathScreen;


    private float initialMass;
    private float initialDrag;
    Stopwatch timer;


    private void Awake()
    {
        playerCustomCollider.StayTriggerZone += StayInWater;
        _playerRigidbody = GetComponentInParent<Rigidbody>();

        initialMass = _playerRigidbody.mass;
        initialDrag = _playerRigidbody.drag;

        timer = new Stopwatch();
    }

    private void Update()
    {
        //if (_isSubmerged)
        //TimeCalculator();

        if (_isSubmerged)
            UnityEngine.Debug.Log($"Time in seconds is: {timer.Elapsed.TotalSeconds}");

        if (timer.Elapsed.TotalSeconds > maxTimeSubmerged)
            KillPlayer();
    }

    void StayInWater(Collider collider)
    {

        if (collider.CompareTag(waterTag))
        {
            // if we touched the water
            //Debug.Log(waterCollider.name + " TOUCHED " + collider.name);

            if (collider.bounds.Contains(waterCollider.bounds.min) && collider.bounds.Contains(waterCollider.bounds.max))
            {
                if (!_isSubmerged)
                {
                    UnityEngine.Debug.Log("Immersed."); // debug

                    // start timer and set bool when submerged
                    timer.Start();
                    _isSubmerged = true;

                    // set player parameters
                    _playerRigidbody.mass = 0.5f;
                    _playerRigidbody.drag = waterDrag;
                }
                
            }
            else
            {
                UnityEngine.Debug.Log("System designed.");
                _isSubmerged = false;
                ResetPlayer();

                // reset timer
                timer.Stop();
                timer.Reset();
            }

        }
    }

    private void KillPlayer()
    {
        player.SetActive(false);
        deathScreen.SetActive(true);

        Time.timeScale = 0; // end game
        timer.Stop();
    }

    private void ResetPlayer()
    {
        _playerRigidbody.mass = initialMass;
        _playerRigidbody.drag = initialDrag;
    }
}
