using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float speed = 5.0f;
    public Vector2 margin;  //뷰포트 좌표
    public VariableJoystick joystick;

    public float Rspeed = 150;
    float angleX;

    //중력적용
    public float gravity = -20;
    float velocityY;    //낙하속도(벨로시티는 방향과 힘을 들고 있다)
    float jumpPower = 10; //점프파워
    int jumpCount = 0; //점프카운트

    CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        margin = new Vector2(0, 0);
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
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

        dir = Camera.main.transform.TransformDirection(dir);
        // transform.position += dir * speed * Time.deltaTime;

        if (cc.collisionFlags == CollisionFlags.Below)
        {
            velocityY = 0;
            jumpCount = 0;
        }
        else
        {
            //땅에 닿지 않은 상태이기때문에 중력적용하기
            velocityY += gravity * Time.deltaTime;
            dir.y = velocityY;
        }
        if (Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            jumpCount++;
            velocityY = jumpPower;
        }

        cc.Move(dir * speed * Time.deltaTime);

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
}