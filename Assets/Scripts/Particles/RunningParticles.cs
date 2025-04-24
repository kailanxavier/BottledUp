using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningParticles : MonoBehaviour
{
    Player _player;
    InputManager _inputManager;
    ParticleSystem runningSystem;

    void Awake()
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
        bool isGrounded = _player.IsGrounded;
        bool isMoving = _player.CurrentSpeed > 0.5f;

        if (isGrounded && isMoving)
        {
            runningSystem.Play(); // If isGrounded play the particles
        } 
    }
}
