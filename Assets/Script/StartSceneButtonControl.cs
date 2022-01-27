using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneButtonControl : MonoBehaviour
{

    public void changetogamescene()
    {
        SceneManager.LoadScene(1);
    }

    public void quitgame()
    {
        Application.Quit();
        Debug.Log("離開遊戲");  //Unity測試用
    }

}
