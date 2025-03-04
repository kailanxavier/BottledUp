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
        HandleJumpAnimation();
    }

    void HandleMoveAnimation()
    {
        Vector2 inputVector = _inputManager.GetInputVectorNormalized();
        if (_player.IsGrounded())
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

    void HandleJumpAnimation()
    {
        if (_inputManager.CheckForJump())
        {
            _animator.Play("Jump");
        }
    }
}
