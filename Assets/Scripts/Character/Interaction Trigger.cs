using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionTrigger : MonoBehaviour {

    public GameObject myObject;
    public PlayerController manager;

    private void OnTriggerEnter(Collider collider) {
        myObject = collider.gameObject;

        manager = myObject.GetComponent<PlayerController>();
        if(!manager) return;

        Type t = typeof(InteractingState);
        manager.FSM.NextState(t);
    }
}
