using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsLoadingScript : MonoBehaviour
{
    [SerializeField] private Transform _levelsParent;
    private void Awake() 
    {
        ActivatePassedLevels();
    }
    private void ActivatePassedLevels()
    {
        var level = SaveScript.Instance.GetLevel();
        for (int i = 0; i < level; i++)
        {
            var child = _levelsParent.GetChild(i);
            var color = child.GetComponent<Image>().color;
            child.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 1f);
            child.GetComponent<Button>().enabled = true;
        }
    }
}
