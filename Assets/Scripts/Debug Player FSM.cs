using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugPlayerFSM : MonoBehaviour {

    [SerializeField] TMP_Text messageText;

    public void onStateChanged(IState state) {
        
        string message = "Current State : ";
        
        switch(state) {
            case WalkingState: message += "Walking";
                break;
            case DummyState: message += "Dummy";
                break;
        }

        messageText.text = message;
    }
}
