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

    public float Rspeed = 150;
    float angleX;

    enum State
    {
        Idle, Walk, Run, Attack1, Attack2, Hit, Die
    }

    State state;
    Animator Anim;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;
        margin = new Vector2(0, 0);
        Anim=GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
        Animation();
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


    private void Rotate()
    {
        float h = Input.GetAxis("Mouse X");

        if (h == 0)
        {
            h = joystick.Horizontal;
        }

        //회전각도를 직접 제어
        angleX += h * Rspeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, angleX, 0);
    }

    private void Animation()
    {
        switch (state)
        {
            case State.Idle:
                Anim.SetTrigger("Idle");
                break;
            case State.Walk:
                Anim.SetTrigger("Walk");
                break;
            case State.Run:
                Anim.SetTrigger("Run");
                break;
            case State.Attack1:
                Anim.SetTrigger("Attack1");
                break;
            case State.Attack2:
                Anim.SetTrigger("Attack2");
                break;
            case State.Hit:
                Anim.SetTrigger("Hit");
                break;
            case State.Die:
                Anim.SetTrigger("Die");
                break;
        }
    }
}