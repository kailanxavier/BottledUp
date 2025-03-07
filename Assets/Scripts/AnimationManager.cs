using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    InputManager _inputManager;
    Animator _animator;
    Player _player;

    private void Start()
    {
        _inputManager = GetComponent<InputManager>();
        _player = GetComponent<Player>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        HandleMoveAnimation();
    }

    void HandleMoveAnimation()
    {
        Vector2 inputVector = _inputManager.GetInputVectorNormalized();
        if (_player.isGrounded && _player.canMove)
        {
            _animator.SetFloat("Horizontal", Math.Abs(inputVector.x));
            _animator.SetFloat("Vertical", Math.Abs(inputVector.y));
        }
        else
        {
            _animator.SetFloat("Horizontal", 0f);
            _animator.SetFloat("Vertical", 0f);
        }
    }

    public void HandleJumpAnimation()
    {
        // If player pressed key, is grounded and can jump then play the jump animation
        if (_inputManager.CheckForJump() && _player.isGrounded && _player.canJump)
        {
            _animator.Play("Jump");
        }
    }

    public void HandleAttackAnimation()
    {
        _animator.Play("Attack");
    }
}
