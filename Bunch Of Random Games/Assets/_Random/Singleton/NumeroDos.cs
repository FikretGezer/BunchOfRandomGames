using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumeroDos : MonoBehaviour
{
    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            var boolean = EmptyClass.Instance.Deneme;
            boolean = boolean ? false : true;
            EmptyClass.Instance.Deneme = boolean;
            Debug.Log(EmptyClass.Instance.Deneme);
        }
            
    }
}
