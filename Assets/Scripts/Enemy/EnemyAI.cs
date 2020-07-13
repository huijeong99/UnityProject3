using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


//몬스터 정보 구조체
struct EInfo
{
    public float HP;
    public float attack;
    public float defend;
    public float castingTime;
    public float speed;        //이속
    public float distance;     //플레이어 인식범위
    public float attDistance;   //공격인식범위
}


public class EnemyAI : MonoBehaviour
{
    EInfo eInfo;            //몬스터 정보 구조체 불러오기
    float currentHP;
    
    [SerializeField] GameObject player;   //거리계산을 위한 플레이어
    Vector3 startPoint;         //몬스터 시작지점

    NavMeshAgent agent;

    //Animator enemyAnim;         //애니메이터 받아오기
    //레거시 애니메이션으로 처리한다
    public AnimationClip idle;
    public AnimationClip trace;
    public AnimationClip returned;
    public AnimationClip hit1;
    public AnimationClip hit2;
    public AnimationClip casting;
    public AnimationClip attack1;
    public AnimationClip attack2;
    public AnimationClip attack3;
    public AnimationClip die;

    Animation anime;

    float currCastingTime=0;          //캐스팅 시간

    enum enemyType
    {
        Basic, Beast, Undead, Ghost
    }

    enum State
    {
        Idle, Trace, Return, Casting, Attack, Hit, Die
    }

    State state;        //몬스터 상태


    //HP바
    GameObject hpBar;
    public GameObject hpBarPre;
    public Vector3 hpBarOffset = new Vector3(0, 2.0f, 0);

    private Canvas uiCanvas;
    private Image hpBarImage;


    // Start is called before the first frame update
    void Start()
    {
        startPoint = this.transform.position;             //이동 시작지점 저장해두기
        player = GameObject.Find("Player").gameObject;    //추적할 플레이어 컴포넌트 가져오기

        agent = GetComponent<NavMeshAgent>();   //네비메시 가져오기
        agent.enabled = false;                  //Move상태에서만 이동해야 하기 때문에 기본적으로 비활성화해둔다

        //enemyAnim = GetComponent<Animator>();   
        anime = GetComponent<Animation>();
        anime.clip = idle;
        anime.Play();

        state = State.Idle;

        SetEnemy();
        currentHP = eInfo.HP;

        setHPBar();
    }

    // Update is called once per frame
    void Update()
    {
        setAction();
        setAnime();

        setHP();
    }

    private void SetEnemy()
    {
        if (gameObject.name.Contains("warrior"))
        {
            eInfo.HP = 30;
            eInfo.attack = 10;
            eInfo.defend = 20;
            eInfo.castingTime = 0.0f;
            eInfo.speed = 5.0f;
            eInfo.distance = 10.0f;
            eInfo.attDistance = 1.5f;
        }

        else if (gameObject.name.Contains("archer"))
        {
            eInfo.HP = 20;
            eInfo.attack = 15;
            eInfo.defend = 10;
            eInfo.castingTime = 0.0f;
            eInfo.speed = 3.0f;
            eInfo.distance = 15.0f;
            eInfo.attDistance = 4.0f;
        }

        else if (gameObject.name.Contains("dragon"))
        {
            eInfo.HP = 50;
            eInfo.attack = 20;
            eInfo.defend = 30;
            eInfo.castingTime = 3.0f;
            eInfo.speed = 6.0f;
            eInfo.distance = 5.0f;
            eInfo.attDistance = 2.0f;
        }

        else if (gameObject.name.Contains("ghost"))
        {
            eInfo.HP = 10;
            eInfo.attack = 40;
            eInfo.defend = 5;
            eInfo.castingTime= 5.0f;
            eInfo.speed = 5.0f;
            eInfo.distance = 15.0f;
            eInfo.attDistance = 2.0f;
        }

        else if (gameObject.name.Contains("mummy"))
        {
            eInfo.HP = 15;
            eInfo.attack = 30;
            eInfo.defend = 5;
            eInfo.castingTime = 4.0f;
            eInfo.speed = 1.0f;
            eInfo.distance = 2.0f;
            eInfo.attDistance = 1.5f;
        }

        else if (gameObject.name.Contains("orc"))
        {
            eInfo.HP = 70;
            eInfo.attack= 30;
            eInfo.defend = 50;
            eInfo.castingTime = 6.0f;
            eInfo.speed = 3.0f;
            eInfo.distance = 3.0f;
            eInfo.attDistance = 2.0f;
        }

        else if (gameObject.name.Contains("werewolf"))
        {
            eInfo.HP = 55;
            eInfo.attack = 45;
            eInfo.defend = 15;
            eInfo.castingTime = 3.0f;
            eInfo.speed = 10.0f;
            eInfo.distance = 20.0f;
            eInfo.attDistance= 1.5f;
        }

        else if (gameObject.name.Contains("zombie"))
        {
            eInfo.HP = 60;
            eInfo.attack = 10;
            eInfo.defend = 5;
            eInfo.castingTime = 2.0f;
            eInfo.speed = 2.0f;
            eInfo.distance = 5.0f;
            eInfo.attDistance = 1.5f;
        }

        else
        {
            return;
        }
    }

    private void setAction()
    {
        //상태와 무관하게 HP가 0이 될 경우 죽는다
        if (eInfo.HP <= 0)
        {
            //hpBarImage.GetComponentsInChildren<Image>()[1].color = Color.clear;//HP바 색 초기화
            state = State.Die;
        }

        //플레이어가 일정 거리 내에 존재할 경우 추적한다
        if (Vector3.Distance(player.transform.position, transform.position) < eInfo.distance)//거리 조정해주기
        {
            state = State.Trace;
        }

        if (state == State.Trace)
        {
            //플레이어 위치를 인식해 다가온다
            //유령의 경우 순간이동하며 아닐 경우 서서히 이동한다
            if (this.name.Contains("ghost"))
            {
                ghostTel();
            }

            else
            {
                followPlayer();
            }
        }

        //시작 위치에서 지나치게 벗어났을 경우 원래 위치로 돌아간다
        if (Vector3.Distance(transform.position, startPoint) > eInfo.distance)
        {
            state = State.Return;
        }
        if (state == State.Return) { returnToStart(); }

        //플레이어가 공격범위 내로 들어왔을 경우 공격상태로 전환한다
        else if (Vector3.Distance(transform.position, player.transform.position) < eInfo.attDistance)
        {
            state = State.Attack;
        }
        if (state == State.Casting)
        {
            if (agent.enabled) { agent.enabled = false; }   //추적을 멈춤
            skillCast();
        }

        if (state == State.Attack)
        {
            if (agent.enabled) { agent.enabled = false; }   //추적을 멈춤
            transform.LookAt(player.transform);
        }//공격시 플레이어를 바라보게 설정 


        if (state == State.Die)
        { //적이 사망할 시 일정시간(4초) 이후 삭제한다
            Destroy(hpBar, 0.9f);
            Destroy(this.gameObject, 0.9f);
        }
    }

    //애니메이션 세팅
    private void setAnime()
    {
        switch (state)
        {
            case State.Idle:
                anime.CrossFade(idle.name, 0.3f);
                break;

            case State.Trace:
                anime.CrossFade(trace.name, 0.3f);
                break;

            case State.Return:
                anime.CrossFade(returned.name, 0.3f);
                break;

            case State.Casting:
                anime.CrossFade(casting.name, 0.3f);
                break;

            case State.Attack:
                int num = UnityEngine.Random.Range(1, 3);

                switch (num)
                {
                    case 1:
                        anime.CrossFade(attack1.name, 0.3f);
                        break;
                    case 2:
                        anime.CrossFade(attack2.name, 0.3f);
                        break;
                    case 3:
                        anime.CrossFade(attack3.name, 0.3f);
                        break;
                }
                break;

            case State.Hit:
                int hitType = UnityEngine.Random.Range(0, 1);

                if (hitType == 0)
                {
                    anime.CrossFade(hit1.name, 0.1f);
                }
                else
                {
                    anime.CrossFade(hit2.name, 0.1f);
                }

                break;
            case State.Die:
                anime.CrossFade(die.name, 0.3f);
                break;
        }
    }

    //유령 이동처리
    private void ghostTel()
    {
        //플레이어가 있는 위치로 즉시 이동한 후 공격상태로 바꾼다
        Vector3 dir = (player.transform.position - transform.position).normalized;
        dir.Normalize();    //플레이어가 있는 방향만 추출한다

        //차후 애니메이션 정리 후 자연스러운 돌아보기 구현하기
        transform.LookAt(player.transform);

        //유령을 투명처리한다, 하위에 있는 meshRenderer 컴포넌트를 추출한다
        MeshRenderer render = GetComponentInChildren<MeshRenderer>();
        render.enabled = false; //일시적으로 비활성화시켜 보이지 않게 만든다

        //플레이어가 있는 위치 바로 앞까지 이동한다
        //이동 후 플레이어와 유지할 거리 지정해주기
        transform.position = player.transform.position;
        render.enabled = true;  //다시 이미지 활성화
       
        //이동 후 즉시 캐스팅
        state = State.Casting;

        //시작 위치에서 지나치게 벗어났을 경우 원래 위치로 돌아간다
        if (Vector3.Distance(transform.position, startPoint) > eInfo.distance)
        {
            state = State.Return;
        }
    }

    //유령 제외 이동처리
    private void followPlayer()
    {
        if (!agent.enabled) { agent.enabled = true; }   //에이전트가 비활성상태일 경우 활성화시켜준다

        agent.SetDestination(player.transform.position);    //플레이어 추적

        //추적 후 일정 거리 내에 들어왔을 경우 캐스팅한다
        //if (enemyInfo.getType() == (int)monsterType.caster)
        //{
        //    state = State.Casting;
        //}
        //
        ////추적 후 일정 거리 내에 들어왔을 경우 공격한다
        //if (enemyInfo.getType() == (int)monsterType.warrior)
        //{
        //    state = State.Attack;
        //}
    }

    //시작 지점으로 돌아가기
    private void returnToStart()
    {
        if (!agent.enabled) { agent.enabled = true; }

        agent.SetDestination(startPoint);
    }

    //스킬 캐스팅
    private void skillCast()
    {
        currCastingTime += 0.1f;//캐스팅 시전 시작

        if (currCastingTime > eInfo.castingTime)
        {
            state = State.Attack;//캐스팅 종료시 공격상태로 바꾼다
            currCastingTime = 0;
        }
    }

    //HPBar세팅
    private void setHPBar()
    {
        uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        hpBar = Instantiate<GameObject>(hpBarPre, uiCanvas.transform);

        hpBarImage = hpBar.GetComponentInChildren<Image>();

        var _hpBar = hpBar.GetComponent<EnemyHPBar>();
        _hpBar.targetTr = this.gameObject.transform;

        _hpBar.offset = hpBarOffset;
    }

    //HP바 변화
    private void setHP()
    {
        //HP변화 적용해주기
        hpBarImage.fillAmount = 3.0f / eInfo.HP;
    }
}
