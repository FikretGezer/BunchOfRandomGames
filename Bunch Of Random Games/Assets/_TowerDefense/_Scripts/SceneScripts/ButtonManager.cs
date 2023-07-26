using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject _canvasMenu;
    public void LoadAScene(string nameOfTheScene)
    {
        if(nameOfTheScene != "")
            SceneManager.LoadScene(nameOfTheScene);
    }
    public void LoadLevels(int level)
    {
        SceneManager.LoadScene(level);
    }
    public void StartLastCameLevel()
    {
        SceneManager.LoadScene("Level " + SaveScript.Instance.GetLevel().ToString());
    }
    public void Quit()
    {
        Application.Quit();
    }
}
