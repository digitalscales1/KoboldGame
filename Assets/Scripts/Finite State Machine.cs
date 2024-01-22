using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class FiniteStateMachine {
    [SerializeField] IState current;
    public IState Current => current;

    [SerializeField] HashSet<IState> states;
    public HashSet<IState> States {
        get { return states; }
        set { states = value; }
    }

    [SerializeField] UnityEvent<IState> onStateChanged;

    public void NextState(IState next) {
        if(next == null) { Debug.Log("null"); return; }

        IState actual;
        bool flag = states.TryGetValue(next, out actual);
        
        if(!flag || actual == null) { Debug.Log("Flag"); return; }

        current?.OnEnd();
        current = actual;

        onStateChanged?.Invoke(current);
        current?.OnStart();
    }

    public void OnStart(IState initial) {
        current = initial;

        onStateChanged?.Invoke(current);
        current?.OnStart();
    }

    public void OnUpdate() {
        IState next = current?.OnUpdate();
        if(next == current) return;

        NextState(next);
    }
}
