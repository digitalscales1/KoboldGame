using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementMachine : MonoBehaviour {

    public enum State {
        Moving, Interacting, Falling, Dead
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
    [SerializeField] private float movementSmoothing = 1.0f;
    [SerializeField] private Vector2 rawInput;
    [SerializeField] private Vector2 smoothInput;
    [SerializeField] private Vector2 smoothVelocity;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("Grounding")]
    [SerializeField] private bool isGrounded;

    private void Update() {
        SwitchingLogic();
    }

    private void SwitchingLogic() {
        
        // Falling has priority
        if(!isGrounded){ state = State.Falling; }

        // swich logic state
        switch(state) {
            case State.Falling: 
                if(!isGrounded) { return; }
                state = State.Moving;
                break;
        }

        // execution logic state
        switch(state) {
            case State.Moving:
                MovingState();
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
