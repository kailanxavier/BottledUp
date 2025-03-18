using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        _animator.SetFloat("Horizontal", 0f);
        _animator.SetFloat("Vertical", 0f);
        if (_player.isGrounded && _player.canMove) HandleMoveAnimation();
    }

    public void HandleMoveAnimation()
    {
        Vector2 inputVector = _inputManager.GetInputVectorNormalized();
        _animator.SetFloat("Horizontal", Math.Abs(inputVector.x));
        _animator.SetFloat("Vertical", Math.Abs(inputVector.y));
    }

    public void HandleJumpAnimation()
    {
        _animator.SetBool("IsGrounded", _player.isGrounded);
    }

    public void HandleAttackAnimation()
    {
        _animator.Play("Attack");
    }
}
