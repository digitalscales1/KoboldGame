using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState {

    public void OnStart();
    public void OnEnd();
    public Type OnUpdate();
}
