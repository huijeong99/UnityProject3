using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string SceneName;
    CharacterController player;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.name=="Player")
        {
            LoadSceneMode();
        }
    }

    public void LoadSceneMode()
    {
        print("씬이동");
        SceneManager.LoadScene(SceneName);  //작성된 씬 이름으로 이동
    }
}
