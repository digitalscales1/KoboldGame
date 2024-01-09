using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour {

    public Transform camera;

    [Header("Refined Movement")]
    public Vector3 direction;
    public float magnitude;

    [Header("Input Movement Settings")]
    public float smoothMovement = 20.0f;
    public Vector2 movement;
    public Vector2 rawMovement;

    public void OnMovement(InputAction.CallbackContext context){
        rawMovement = context.ReadValue<Vector2>();
    }

    void Start() {
        movement = Vector2.zero;
    }

    void Update() {
        movement = Vector2.Lerp(rawMovement, movement, Time.deltaTime * smoothMovement);
        
    }

}
