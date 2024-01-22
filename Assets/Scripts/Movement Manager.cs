using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour {

    [Header("Movement States")]
    [SerializeField] WalkingState walkingState;
    [SerializeField] DummyState dummyState;

    [Header("Movement Finite State Machine")]
    [SerializeField] FiniteStateMachine fsm;

    private void Start() {
        fsm.States = new Dictionary<System.Type, IState> {
            { walkingState.GetType(), walkingState }, { dummyState.GetType(), dummyState }
        };

        fsm.OnStart(walkingState);
    }

    private void Update() {
        fsm.OnUpdate();
    }
}
