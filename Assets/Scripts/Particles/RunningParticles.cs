using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningParticles : MonoBehaviour
{
    Player _player;

    void Start()
    {
        _player = GetComponentInChildren<Player>();
    }

    void Update()
    {
        HandleParticles();
    }


    void HandleParticles()
    {
        bool isGrounded = _player.isGrounded;
    }
}
