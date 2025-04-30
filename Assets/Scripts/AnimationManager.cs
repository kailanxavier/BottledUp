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

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _player = GetComponent<Player>();
        _animator = GetComponentInChildren<Animator>();

        _inputManager.AttackPerformed += HandleAttackAnimation;
    }

    private void Update()
    {
        if (_player.IsGrounded) HandleMoveAnimation();
    }

    public void HandleMoveAnimation()
    {
        Vector2 inputVector = _inputManager.InputVector;
        _animator.SetFloat("Horizontal", Math.Abs(inputVector.x), 0.2f, Time.deltaTime);
        _animator.SetFloat("Vertical", Math.Abs(inputVector.y), 0.2f, Time.deltaTime);

        if (inputVector != Vector2.zero)
        {
            _animator.SetBool("IsSprinting", _player.IsSprinting);
        }
        else
        {
            _animator.SetBool("IsSprinting", false);
        }
    }

    public void HandleSlamAnimation()
    {
        _animator.Play("Slam");
    }

    public void HandleAttackAnimation()
    {
        _animator.Play("Attack");
    }

    public void HandleJumpAnimation()
    {
        _animator.Play("Jump");
    }
}
