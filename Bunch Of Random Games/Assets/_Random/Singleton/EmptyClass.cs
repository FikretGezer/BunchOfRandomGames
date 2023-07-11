using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyClass : SingletonPersistent<EmptyClass>
{
    protected override void Awake() {
        base.Awake();
    }
    public bool Deneme {get; set;}
}
