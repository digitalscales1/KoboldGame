using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIDialogueController : MonoBehaviour, IInteractable {

    [Header("Dialogue Nodes")]
    BaseNode idle = new BaseNode();
    [SerializeField] List<BaseNode> dialogue = new List<BaseNode>();

    [Header("Dialogue Finite State Machine")]
    [SerializeField] FiniteStateMachine fsm;

    [Header("Events")]
    [SerializeField] UnityEvent<BaseNode> onDialogueChange;

    public FiniteStateMachine FSM => fsm;

    public void Start() {
        fsm.States = new Dictionary<System.Type, IState>();
        foreach(BaseNode node in dialogue) {
            fsm.States.Add(node.GetType(), node);
        }

        fsm.OnStart(dialogue[0]);
    }

    public void OnInteraction() {

    }

    public void OnOptionChosen(BaseOption option) {
        Debug.Log("chosen: " + option.ToString());
    }
}
