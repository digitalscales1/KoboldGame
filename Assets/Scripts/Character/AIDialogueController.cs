using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDialogueController : MonoBehaviour, IInteractable {

    [Header("Dialogue Nodes")]
    [SerializeField] BaseNode baseNode;

    [Header("Dialogue Finite State Machine")]
    [SerializeField] FiniteStateMachine fsm;
    public FiniteStateMachine FSM => fsm;

    public void OnInteraction() {
        throw new System.NotImplementedException();
    }
}
