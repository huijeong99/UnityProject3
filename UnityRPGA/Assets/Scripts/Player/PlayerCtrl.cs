using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float speed = 5.0f;
    public Vector2 margin;  //뷰포트 좌표
    public VariableJoystick joystick;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        margin = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h == 0 && v == 0)
        {
            h = joystick.Horizontal;
            v = joystick.Vertical;
        }

        Vector3 dir = new Vector3(h, 0, v);//3차원 이동

        transform.position += dir * speed * Time.deltaTime;
    }
}
