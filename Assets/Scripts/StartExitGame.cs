using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class StartExitGame : MonoBehaviour
{
    public Image fade;
    float fades = 1.0f;
    bool fadeOn = false;
    bool fadeOff = true;

    private void Update()
    {
        if (fadeOn == true)
        {
            StartGame();
        }

        if (fadeOff == true)
        {
            ShowScene();
        }
    }

    public void StartGame()
    {
        fades += 0.3f * Time.deltaTime;
        fade.color = new Color(0, 0, 0, fades);

        if (fades >= 1.0f)
        {
            SceneManager.LoadScene("StartVillage");
        }
    }

    private void ShowScene()
    {
        fades -= 0.3f * Time.deltaTime;
        fade.color = new Color(0, 0, 0, fades);

        if (fades <= 0)
        {
            fades = 0;
            fadeOff = false;
        }
    }

    public void FadeOn()
    {
        fadeOn = true;
    }

    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;    //에디터 종료
        Application.Quit();
    }
}
