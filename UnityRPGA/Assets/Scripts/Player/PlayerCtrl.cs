using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

struct PInfo
{
    public int lv;
    public int maxExp;
    public int currExp;
    public int attack;
    public int defend;
    public float sp;

}

public class PlayerCtrl : MonoBehaviour
{
    PInfo playerInfo;   //플레이어 정보

    //플레이어 움직임
    public Vector2 margin;  //뷰포트 좌표

    public VariableJoystick joystick;
    public RectTransform backGround;    //조이스틱 백그라운드
    public RectTransform handle;      //조이스틱 핸들부분
    float distance;                   //조이스틱이 움직인 정도

    public float Rspeed = 180;  //카메라 회전속도
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
    State state;            //플레이어 기본 상태
    enum DebuffState
    {
        Bleedinig, Infection, Fear, Burn, Bind
    }
    DebuffState debuff;     //플레이어 상태이상

    public List<GameObject> playerChar = new List<GameObject>();  //조정할 플레이어 모델
    Animator Anim;          //플레이어 애니메이션

    private void Awake()
    {
        setPlayer sPlayer=null;  //임의적으로 플레이어 데이터를 가져오기 위해 가져올 클래스
        sPlayer = new Knight();

        //현재 활성화 되어있는 플레이어의 애니메이터만 가져오기
        for (int i = 0; i < playerChar.Count; i++)
        {
            if (playerChar[i].activeSelf == true)
            {
                Anim = playerChar[i].GetComponent<Animator>();
        
                //활성화된 플레이어의 이름에 따라 플레이어 정보 불러오기
                switch (playerChar[i].name)
                {
                    case "knight"://대문자에 주의할것
                        sPlayer = new Knight();
                        break;
                    case "Magician":
                        sPlayer = new Magician();
                        break;
                    case "Priest":
                        sPlayer = new Priest();
                        break;
                }
            }
        }
       
        //사전에 정해진 플레이어 데이터 삽입
        playerInfo.lv = sPlayer.getlv();
        playerInfo.maxExp = sPlayer.getmaxExp();
        playerInfo.currExp = sPlayer.getcurrExp();
        playerInfo.attack = sPlayer.getattack();
        playerInfo.defend = sPlayer.getdefend();
        playerInfo.sp = sPlayer.getsp();
    }

    // Start is called before the first frame update
    void Start()
    {
        margin = new Vector2(0, 0);
        cc = GetComponent<CharacterController>();   //캐릭터 컨트롤러 가져오기
        state = State.Idle; //플레이어 초기상태 가져오기
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
        distance = 1;//조이스틱을 사용하지 않을 때 기본적으로 1의 최고속도로 움직인다

        if (h == 0 && v == 0)
        {
            h = joystick.Horizontal;
            v = joystick.Vertical;
            //조이스틱을 당긴 정도에 따라 0~1사이의 값을 출력함(50은 조이스틱의 반지름)
            distance = Vector2.Distance(backGround.position, handle.position) / 50;
        }


        Vector3 dir = new Vector3(h, 0, v);//3차원 이동

        dir = Camera.main.transform.TransformDirection(dir);
        //transform.position += dir * speed * Time.deltaTime;


        //점프안됨 수정필요함
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

        cc.Move(dir * playerInfo.sp * distance * Time.deltaTime);
    }

    //회전
    private void Rotate()
    {
        float rot = Input.GetAxis("Mouse X");

        if (rot == 0)
        {
            rot = joystick.Horizontal;//키보드를 사용하지 않을 때 조이스틱 움직임에 따라 캐릭터가 자동으로 회전함
        }

        //회전각도를 직접 제어
        angleX += rot * Rspeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(0, angleX, 0);
    }

    //애니메이션 찾기
    private void FindAnimation()
    {
        //기본전환
        if (distance == 0)
        {
            state = State.Idle;
        }

        //걷기 전환
        else if (distance < 0.5)
        {
            state = State.Walk;
        }

        //뛰기 전환
        else if (distance >= 0.5)
        {
            state = State.Run;
        }

        //공격1(숫자 1키)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            state = State.Attack1;
        }

        //공격2(숫자 2키)
        if (Input.GetKeyDown(KeyCode.Alpha2))
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

    //지금은 안씀
    //public int playerLvSetting()
    //{
    //    return playerInfo.lv;
    //}
}