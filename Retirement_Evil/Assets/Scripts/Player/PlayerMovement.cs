using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _slowSpeed = 2f;

    private Vector2 _movement;

    private Rigidbody2D _rb;
    private Animator _animator;

    private const string _HORIZONTAL = "Horizontal";
    private const string _VERTICAL = "Vertical";
    private const string _LASTHORIZONTAL = "LastHorizontal";
    private const string _LASTVERTICAL = "LastVertical";

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update() {
        _movement.Set(InputManager.Movement.x, InputManager.Movement.y);
        
        float currentSpeed = InputManager.SlowMovement ? _slowSpeed : _moveSpeed;

        _rb.velocity = _movement * currentSpeed;

        _animator.SetFloat(_HORIZONTAL, _movement.x);
        _animator.SetFloat(_VERTICAL, _movement.y);

        if (_movement != Vector2.zero) {
            _animator.SetFloat(_LASTHORIZONTAL, _movement.x);
            _animator.SetFloat(_LASTVERTICAL, _movement.y);
        }
    }
}
