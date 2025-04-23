using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningParticles : MonoBehaviour
{
    Player _player;
    InputManager _inputManager;
    ParticleSystem runningSystem;

    void Start()
    {
        _player = GetComponentInParent<Player>();
        _inputManager = GetComponentInParent<InputManager>();
        runningSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        HandleParticles();
    }


    void HandleParticles()
    {
        Vector2 inputVector = _inputManager.InputVector;

        //bool isGrounded = _player.isGrounded;

        // Check if player is moving
        bool isMoving = Math.Abs(inputVector.x) > 0 || Math.Abs(inputVector.y) > 0;

        //if (isGrounded && isMoving) runningSystem.Play(); // If isMoving and isGrounded play the particles

        Debug.Log("Is Moving: " + isMoving);
    }
}
