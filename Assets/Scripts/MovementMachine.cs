using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementMachine : MonoBehaviour {

    public enum State {
        Moving, Interacting, Falling, EndFalling, Dead
    };

    [Header("State Machine")]
    [SerializeField] private State state;
    public State GetState () => state;
    public void SetState (State other) { state = other; }

    [Header("Interaction")]
    [SerializeField] private GameObject other;

    [Header("Movement")]
    [SerializeField] private CharacterAttributes attributes;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float movementSmoothing = 25.0f;
    private Vector2 rawInput;
    private Vector2 smoothInput;
    private Vector2 smoothVelocity;

    [Header("Falling")]
    [SerializeField] private float gravity;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("Grounding")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform middlePoint;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float distance;

    [SerializeField] private float groundingSmoothing = 10.0f;
    [SerializeField] private float groundingMinDistance = 1.5f;
    [SerializeField] private float fallingMinDistance = 1.1f;


    public GameObject obj;

    private void Update() {

        animator.SetBool("isFalling", state == State.Falling);

        SwitchingLogic();
        Grounding();
    }

    private void SwitchingLogic() {
        
        // Falling has priority
        if(!isGrounded){ state = State.Falling; }

        // swich logic state
        switch(state) {
            case State.Falling: 
                if(isGrounded) { state = State.EndFalling; }
                break;

            case State.EndFalling:
                state = State.Moving;
                break;
        }

        // execution logic state
        switch(state) {
            case State.Moving:
                MovingState();
                break;

            case State.Falling:
                FallingState();
                break;

            case State.EndFalling:
                EndFallingState();
                break;
        }
    }

    private void MovingState() {
        smoothInput = Vector2.SmoothDamp(smoothInput, rawInput, ref smoothVelocity, Time.deltaTime * movementSmoothing);
        Vector3 direction = new Vector3(smoothInput.x, 0.0f, smoothInput.y);
        float magnitude = Mathf.Clamp01(Mathf.Abs(smoothInput.x)+Mathf.Abs(smoothInput.y));

        if(magnitude > 0.1f) {
            Quaternion look = Quaternion.LookRotation(direction);
            Quaternion rot = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * attributes.RotationSpeed * magnitude);

            transform.rotation = rot;
        }

        rb.velocity = direction * magnitude * attributes.MovementSpeed;
        animator.SetFloat("magnitude", magnitude);
    }

    private void FallingState() {
        gravity += Physics.gravity.y * Time.deltaTime;

        Vector3 velocity = new Vector3(rb.velocity.x, gravity, rb.velocity.z);
        rb.velocity = velocity;
    }

    private void EndFallingState() {
        gravity = 0.0f;
        
        Vector3 velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
        rb.velocity = velocity;
    }

    private void Grounding() {
        RaycastHit hit;
        Ray ray = new Ray(middlePoint.position, Vector3.down);
        if(Physics.SphereCast(ray, 0.3f, out hit, 3.0f, groundMask)){

            distance = middlePoint.position.y - hit.point.y;

            if(isGrounded) { isGrounded = distance <= groundingMinDistance; } 
            else { isGrounded = distance <= fallingMinDistance; }

        } else {
            isGrounded = false;
        }

        // stick to ground
        if (isGrounded && state == State.Moving){ 
            float y = Mathf.Lerp(hit.point.y, transform.position.y, groundingSmoothing);
            Vector3 pos = new Vector3(transform.position.x, hit.point.y, transform.position.z);
            transform.position = pos;
        }
    }

    public void OnInteraction(GameObject gameObject, bool flag) {
        if(other == null) {
            
            if(state != State.Moving) { return; }
            
            state = State.Interacting;
            other = gameObject;
        } else {
            if(flag == true || other != gameObject){ return; }

            state = State.Moving;
            other = null;
        }
    }

    public void OnMovement(InputAction.CallbackContext context) {
        rawInput = context.ReadValue<Vector2>();
    }

    public void OnDead(GameObject gameObject){

    }
}
