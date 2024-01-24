using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ICharacterController {

    [Header("Movement States")]
    [SerializeField] WalkingState walkingState;
    [SerializeField] InteractingState interactingState;
    [SerializeField] DyingState dyingState;

    [Header("Movement Finite State Machine")]
    [SerializeField] FiniteStateMachine fsm;
    public FiniteStateMachine FSM => fsm;

    private void Start() {
        fsm.States = new Dictionary<System.Type, IState> {
            { walkingState.GetType(), walkingState }, { interactingState.GetType(), interactingState}, {dyingState.GetType(), dyingState}
        };

        fsm.OnStart(walkingState);
    }

    private void Update() {
        fsm.OnUpdate();
    }
}
