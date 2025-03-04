using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    InputManager _inputManager;
    Animator _animator;

    private void Start()
    {
        _inputManager = GetComponent<InputManager>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        HandleMoveAnimation();
    }

    void HandleMoveAnimation()
    {
        Vector2 inputVector = _inputManager.GetInputVectorNormalized();
        _animator.SetFloat("Horizontal", Math.Abs(inputVector.x));
        _animator.SetFloat("Vertical", Math.Abs(inputVector.y));
    }
}
