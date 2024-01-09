using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMachine : MonoBehaviour {

    private enum State {
        Falling, Moving, Talking, Dead
    };

    [Header("Movement State Machine")]
    [SerializeField] private State previous;
    [SerializeField] private State current;

    [SerializeField] private bool isGrounded;


    [Header("Movement")]
    [SerializeField] private CharacterAttributes attributes;
    [SerializeField] private Rigidbody rb;

    private void Update() {
        SwitchingLogic();
    }

    private void SwitchingLogic(){
        // next state logic
        switch(previous) {
            case State.Falling: 
                if(!isGrounded) { return; }
                current = State.Moving;
                break;

            default: return;
        }

        // current state logic
        switch(current) {
            case State.Moving:
                MovingState();
                break;

            default: return;
        }

        previous = current;
    }

    private void MovingState() {
        rb.velocity = new Vector3(1.0f, 0.0f, 0.0f);
    }
}
