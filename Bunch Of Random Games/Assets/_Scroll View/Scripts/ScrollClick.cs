using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollClick : MonoBehaviour
{
    public Image _changingImage;
    public void Clicked()
    {
        var current = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite;
        if(_changingImage.sprite != current)
        {
            _changingImage.sprite = current;
            Debug.Log("<color=cyan> Clicked </color>");
        }
    }
}
