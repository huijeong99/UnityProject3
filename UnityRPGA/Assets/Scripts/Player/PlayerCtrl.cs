using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    //플레이어 움직임
    public float speed = 5.0f;
    public Vector2 margin;  //뷰포트 좌표
    public VariableJoystick joystick;

    public float Rspeed = 150;
    float angleX;

    float h;    //x좌표
    float v;    //z좌표

    //중력적용
    public float gravity = -20;
    float velocityY;    //낙하속도(벨로시티는 방향과 힘을 들고 있다)
    float jumpPower = 10; //점프파워
    int jumpCount = 0; //점프카운트

    CharacterController cc;

    //플레이어 애니메이션
    enum State
    {
        Idle, Walk, Run, Attack1, Attack2, Hit, Die
    }

   public List<GameObject> playerChar = new List<GameObject>();  //조정할 플레이어 모델
    State state;            //플레이어 상태
    Animator Anim;          //플레이어 애니메이션

    // Start is called before the first frame update
    void Start()
    {
        margin = new Vector2(0, 0);
        cc = GetComponent<CharacterController>();   //캐릭터 컨트롤러 가져오기
        state = State.Idle; //플레이어 초기상태 가져오기

        //현재 활성화되어있는 플레이어 찾기
       

        //현재 활성화 되어있는 플레이어의 애니메이터만 가져오기
       // Anim = playerChar.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();     //움직임 처리
        Rotate();   //회전 처리
        FindAnimation();    //애니메이션 찾기
        SetAnimation();     //애니메이션 세팅
    }

    private void Move()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

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

    //회전
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

    //애니메이션 찾기
    private void FindAnimation()
    {
        //걷기 전환


        //뛰기 전환


        //공격1
        if (Input.GetKeyDown(KeyCode.Z))
        {
            state = State.Attack1;
        }

        //공격2
        if (Input.GetKeyDown(KeyCode.X))
        {
            state = State.Attack2;
        }

        //적과 충돌할시 hit
    }

    //애니매이션 설정
    private void SetAnimation()
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