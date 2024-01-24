using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[Serializable] public class InteractingState : BaseState {

    [Header("Dependencies")]
    [SerializeField] GameObject target;
    [SerializeField] CharacterAttributes attributes;
    [SerializeField] Animator anim;

    [Header("Parameters")]
    [SerializeField] float rotationSpeed;

    public override void OnStart() {
        // Do nothing
    }

    public override void OnEnd() {
        // Do nothing
    }

    public override Type OnUpdate() {
        
        if (!target) return this.GetType();

        // Rotate character, then do nothing
        Vector3 direction = target.transform.position - Parent.transform.position;
        float angle = Vector3.Dot(Parent.transform.forward, direction);
        if (angle < 0.9f) RotateCharacer(direction);

        return this.GetType();
    }

    private void RotateCharacer(Vector3 direction) {
        Quaternion look = Quaternion.LookRotation(direction.normalized);
        Quaternion rot = Quaternion.Slerp(Parent.transform.rotation, look, Time.deltaTime * attributes.RotationSpeed);

        Parent.transform.rotation = rot;
        anim.SetFloat("magnitude", 0.5f);
    }

    public void OnInteractionTarget(GameObject gameObject) {
        target = gameObject;
    }
}
