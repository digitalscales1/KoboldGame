using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public class DummyState : BaseState {

    [SerializeField] CharacterController controller;

    override public void OnEnd() {
        // Do nothing
    }

    override public void OnStart() {
        // Do nothing
    }

    override public Type OnUpdate() {
        return this.GetType();
    }
}
