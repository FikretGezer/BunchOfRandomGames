using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveScript : MonoBehaviour
{
    public static SaveScript Instance;
    private void Awake() {
        if(Instance == null)
            Instance = new SaveScript();
    }
    public void SaveLevel(int currentLevel)
    {
        PlayerPrefs.SetInt("MaxLevel", currentLevel);
        PlayerPrefs.Save();
    }
    public int GetLevel()
    {
        if(PlayerPrefs.HasKey("MaxLevel"))
            return PlayerPrefs.GetInt("MaxLevel");

        return 1;
    }
}
