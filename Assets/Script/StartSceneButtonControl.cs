using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneButtonControl : MonoBehaviour
{

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void changeToGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void quitGame()
    {
        Application.Quit();
        Debug.Log("離開遊戲");  //Unity測試用
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            changeToGameScene();
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            quitGame();
        }
    }

}
