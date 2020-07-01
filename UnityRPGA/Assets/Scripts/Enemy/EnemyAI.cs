﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    EnemyInfo enemyInfo;        //적 정보를 담은 클래스 불러오기
    public GameObject player;   //거리계산을 위한 플레이어
    Vector3 startPoint;         //몬스터 시작지점

    NavMeshAgent agent;

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

    // Start is called before the first frame update
    void Start()
    {
        startPoint = this.transform.position;   //이동 시작지점 저장해두기
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;                  //Move상태에서만 이동해야 하기 때문에 기본적으로 비활성화해둔다

        player = GetComponent<GameObject>();    //플레이어 컴포넌트 가져오기

        state = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        setAction();
    }

    private void setAction()
    {
        //상태와 무관하게 HP가 0이 될 경우 죽는다
        if (enemyInfo.getHP() <= 0)
        {
            state = State.Die;
        }

        switch (state)
        {
            case State.Idle:
                //플레이어가 일정 거리 내에 존재할 경우 추적한다
                if (Vector3.Distance(player.transform.position, transform.position) < enemyInfo.getHP())
                {
                    state = State.Trace;
                }
                break;
            case State.Trace:
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

                //추적 후 일정 거리 내에 들어왔을 경우 캐스팅한다
                if (enemyInfo.getType() == (int)monsterType.caster)
                {
                    state = State.Casting;
                }

                //추적 후 일정 거리 내에 들어왔을 경우 공격한다
                if (enemyInfo.getType() == (int)monsterType.warrior)
                {
                    state = State.Attack;
                }
                break;

            case State.Return:
                returnToStart();
                break;

            case State.Casting:
                skillCast();

                break;
            case State.Attack:

                break;
            case State.Hit:
                break;
            case State.Die:
                //적이 사망할 시 일정시간(2초) 이후 삭제한다
                Destroy(this, 2.0f);
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
        if (Vector3.Distance(transform.position, startPoint) > enemyInfo.getDistance())
        {
            state = State.Return;
        }
    }

    //유령 제외 이동처리
    private void followPlayer()
    {
        if (!agent.enabled) agent.enabled = true;   //에이전트가 비활성상태일 경우 활성화시켜준다
        agent.SetDestination(player.transform.position);    //플레이어 추적

        //시작 위치에서 지나치게 벗어났을 경우 원래 위치로 돌아간다
        if (Vector3.Distance(transform.position, startPoint) > enemyInfo.getDistance())
        {
            state = State.Return;
        } 
        //플레이어가 공격범위 내로 들어왔을 경우 공격상태로 전환한다
        else if (Vector3.Distance(transform.position, player.transform.position) < enemyInfo.getAttDistance())
        {
            state = State.Attack;
        }
    }

    //시작 지점으로 돌아가기
    private void returnToStart()
    {
        if (!agent.enabled) agent.enabled = true;

        agent.SetDestination(startPoint);
    }

    //스킬 캐스팅
    private void skillCast()
    {
        currCastingTime += 0.1f;//캐스팅 시전 시작

        if (currCastingTime > enemyInfo.getCastingTime())
        {
            state = State.Attack;//캐스팅 종료시 공격상태로 바꾼다
            currCastingTime = 0;
        }
    }
}
