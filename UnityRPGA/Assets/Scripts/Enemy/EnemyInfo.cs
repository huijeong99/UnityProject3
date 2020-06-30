using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//몬스터 정보 구조체
struct EInfo
{
    public int HP;
    public int attack;
    public int defend;
    public int castingTime;
    public float distance;     //플레이어 인식범위

}

enum enemyType
{
    Basic, Beast, Undead, Ghost
}

public abstract class EnemyInfo : MonoBehaviour
{
   
    //static enemyType monsterType;       //몬스터 타입 지정(한 번 지정된 이후 변하지 않으므로 static으로 선언)
    EInfo eInfo;            //몬스터 정보 구조체 불러오기
    static int monsterType=(int)enemyType.Basic;

    //적 정보 getset
    public int getHP(){return eInfo.HP;}
    public int getAttack(){return eInfo.attack;}
    public int getDefend(){return eInfo.defend;}
    public int getCastingTime(){return eInfo.castingTime;}
    public float getDistance(){return eInfo.distance;}

    public int getMType() { return monsterType; }
}
