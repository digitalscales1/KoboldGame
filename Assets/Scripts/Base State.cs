using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] abstract public class BaseState : IState {

    [Header("Base Class Dependencies")]
    [SerializeField] private GameObject parent;
    public GameObject Parent => parent;

    abstract public void OnStart();
    abstract public void OnEnd();
    abstract public Type OnUpdate();
}
