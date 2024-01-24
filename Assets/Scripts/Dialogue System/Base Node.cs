using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New DialogueNode", menuName = "Dialogue/Node")]
public class BaseNode : ScriptableObject, IState {

    [Header("Text Content")]
    [SerializeField] [TextArea] string text;

    [Header("Dialogue Options")]
    [SerializeField] List<BaseOption> options;

    public void OnStart() {
        throw new System.NotImplementedException();
    }
    public void OnEnd() {
        throw new System.NotImplementedException();
    }

    public Type OnUpdate() {
        throw new NotImplementedException();
    }
}
