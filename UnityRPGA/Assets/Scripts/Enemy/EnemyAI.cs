using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    EnemyInfo enemyInfo;        //적 정보를 담은 클래스 불러오기
    public GameObject player;   //거리계산을 위한 플레이어

    enum enemyType
    {
        Basic, Beast, Undead, Ghost
    }

    enum State
    {
        Idle, Trace, Casting, Attack, Hit, Die
    }

    State state;        //몬스터 상태

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<GameObject>();//플레이어 컴포넌트 가져오기

        state = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        setAnimation();   
    }

    private void setAnimation()
    {

        //플레이어가 일정 거리 내에 존재할 경우 추적한다
        if (Vector3.Distance(player.transform.position, transform.position) < enemyInfo.getHP())
        {
            state = State.Trace;
        }

        //추적 후 일정 거리 내에 들어왔을 경우 공격한다

        //추적 후 일정 거리 내에 들어왔을 경우 캐스팅한다

        //아무떄나 공격당하면 hit으로 바뀜

        //dkanEOsk 
    }

    private void ShowAnimation()
    {
        switch (state)
        {
            case State.Idle:
               
                break;
            case State.Trace:
              
                break;
            case State.Casting:
                break;
            case State.Attack:
                break;
            case State.Hit:
                break;
            case State.Die:
                break;
        }
    }
}
