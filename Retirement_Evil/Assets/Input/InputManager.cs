using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 Movement;
    public static bool SlowMovement;

    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _moveSlowAction;

    private void Awake() {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];
        _moveSlowAction = _playerInput.actions["MoveSlow"];
    }

    private void Update() {
        Movement = _moveAction.ReadValue<Vector2>();
        SlowMovement = _moveSlowAction.ReadValue<float>() > 0; // Verifica che il tasto sia premuto
    }
}
