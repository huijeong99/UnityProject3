using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct EnemyInfo
{
    int HP;
    int attack;
    int defend;
    int castingTime;
    float distance;     //플레이어 인식범위
}

public class EnemyAI : MonoBehaviour
{
    public GameObject player;   //거리계산을 위한 플레이어

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
