using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[Serializable] public class WalkingState : BaseState {

    [Header("Dependencies")]
    [SerializeField] CharacterController controller;
    [SerializeField] CharacterAttributes attributes;
    [SerializeField] Animator anim;
    [SerializeField] Transform cameraTransform;
    [SerializeField] InputActionAsset asset;
    InputAction input;
    InputActionMap action;

    [Header("Movement Parameters")]
    [SerializeField] float smoothing = 25.0f;
    Vector2 smoothInput;
    Vector2 smoothVelocity;

    public override void OnStart() {
        action = asset.FindActionMap("Player");
        input = action.FindAction("Move");      
    }

    public override void OnEnd() {
        controller.SimpleMove(Vector3.zero);
    }

    public override Type OnUpdate() {
        MovementHandling();
        return this.GetType();
    }

    private void MovementHandling() {
        Vector3 direction = InputHandling();
        float magnitude = Mathf.Clamp01(Mathf.Abs(smoothInput.x)+Mathf.Abs(smoothInput.y));

        if(magnitude > 0.1f) {
            Quaternion look = Quaternion.LookRotation(direction);
            Quaternion rot = Quaternion.Slerp(Parent.transform.rotation, look, Time.deltaTime * attributes.RotationSpeed * magnitude);

            Parent.transform.rotation = rot;
        }

        Vector3 move = direction * magnitude * attributes.MovementSpeed;
        anim.SetFloat("magnitude", magnitude);
        controller.SimpleMove(move);
    }

    private Vector3 InputHandling() {
        Vector2 rawInput = input.ReadValue<Vector2>();
        smoothInput = Vector2.SmoothDamp(smoothInput, rawInput, ref smoothVelocity, Time.deltaTime * smoothing);

        Vector3 combined = cameraTransform.forward * smoothInput.y + cameraTransform.right * smoothInput.x;
        Vector3 direction = new Vector3(combined.x, 0.0f, combined.z);
        return direction.normalized;
    }
}
