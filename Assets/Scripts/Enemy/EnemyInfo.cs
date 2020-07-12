using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//몬스터 정보 구조체
struct EInfo
{
    public int HP;
    public int attack;
    public int defend;
    public float castingTime;
    public float speed;        //이속
    public float distance;     //플레이어 인식범위
    public float attDistance;   //공격인식범위
}

class EnemyInfo:MonoBehaviour
{
    public void SetWSk(int hp, int att, int def, float cast, float sp, float dist, float adist)
    {
        hp = 30;
        att = 10;
        def = 20;
        cast = 0.0f;
        sp = 5.0f;
        dist = 10.0f;
        adist = 1.5f;
    }

    public void SetASk(int hp, int att, int def, float cast, float sp, float dist, float adist)
    {
        hp = 20;
        att = 15;
        def = 10;
        cast = 0.0f;
        sp = 3.0f;
        dist = 15.0f;
        adist = 4.0f;
    }


    public void SetDragon(int hp, int att, int def, float cast, float sp, float dist, float adist)
    {
        hp = 50;
        att = 20;
        def = 30;
        cast = 3.0f;
        sp = 6.0f;
        dist = 5.0f;
        adist = 2.0f;
    }


    public void SetGhost(int hp, int att, int def, float cast, float sp, float dist, float adist)
    {
        hp = 10;
        att = 40;
        def = 5;
        cast = 5.0f;
        sp = 5.0f;
        dist = 15.0f;
        adist = 2.0f;
    }


    public void SetMummy(int hp, int att, int def, float cast, float sp, float dist, float adist)
    {
        hp = 15;
        att = 30;
        def = 5;
        cast = 4.0f;
        sp = 1.0f;
        dist = 2.0f;
        adist = 1.5f;
    }


    public void SetOrc(int hp, int att, int def, float cast, float sp, float dist, float adist)
    {
        hp = 70;
        att = 30;
        def = 50;
        cast = 6.0f;
        sp = 3.0f;
        dist = 3.0f;
        adist = 2.0f;
    }


    public void SetWolf(int hp, int att, int def, float cast, float sp, float dist, float adist)
    {
        hp = 55;
        att = 45;
        def = 15;
        cast = 3.0f;
        sp = 10.0f;
        dist = 20.0f;
        adist = 1.5f;
    }

    public void SetZombie(int hp, int att, int def, float cast, float sp, float dist, float adist)
    {
        hp = 60;
        att = 10;
        def = 5;
        cast = 2.0f;
        sp = 2.0f;
        dist = 5.0f;
        adist = 1.5f;
    }

}

