using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    void Start()
    {

    }


    public void Scenechanger(int level)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(level);
    }

    public void Exit()
    {
        //fade.Play("FadeOut", 0, 0f);
        //StartCoroutine(Delay());

        Application.Quit();


    }
}
