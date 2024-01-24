using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public class DyingState : BaseState {
    
    [SerializeField] Animator animator;
    [SerializeField] GameObject ragdoll;

    public override void OnStart() {
        animator.SetBool("isDead", true);
    }

    public override void OnEnd() {
        // Do nothing
    }

    public override Type OnUpdate() {

        return this.GetType();
    }
}
