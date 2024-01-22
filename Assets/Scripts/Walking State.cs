using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable] public class WalkingState : BaseState {

    Vector2 smoothInput;
    Vector2 smoothVelocity;

    [Header("Dependencies")]
    [SerializeField] CharacterController controller;
    [SerializeField] CharacterAttributes attributes;
    [SerializeField] Animator anim;
    [SerializeField] Transform cameraTransform;
    [SerializeField] InputActionAsset asset;
    InputAction input;
    InputActionMap action;

    [Header("Next States")]
    [SerializeField] DummyState next;

    [Header("Movement Parameters")]
    [SerializeField] float smoothing = 25.0f;


    public override void OnStart() {
        action = asset.FindActionMap("Player");
        input = action.FindAction("Move");      
    }

    public override void OnEnd() {
        controller.SimpleMove(Vector3.zero);
    }

    public override IState OnUpdate() {
        Vector2 rawInput = input.ReadValue<Vector2>();
        smoothInput = Vector2.SmoothDamp(smoothInput, rawInput, ref smoothVelocity, Time.deltaTime * smoothing);
        Vector3 direction = new Vector3(smoothInput.x, 0.0f, smoothInput.y);
        float magnitude = Mathf.Clamp01(Mathf.Abs(smoothInput.x)+Mathf.Abs(smoothInput.y));

        if(magnitude > 0.1f) {
            Quaternion look = Quaternion.LookRotation(direction);
            Quaternion rot = Quaternion.Slerp(Parent.transform.rotation, look, Time.deltaTime * attributes.RotationSpeed * magnitude);

            Parent.transform.rotation = rot;
        }

        Vector3 move = direction * magnitude * attributes.MovementSpeed;
        controller.SimpleMove(move);

        anim.SetFloat("magnitude", magnitude);

        bool grounded = controller.isGrounded;
        anim.SetBool("isFalling", grounded);

       if (grounded) return next;

        return this;
    }
}
