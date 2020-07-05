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

public enum monsterType
{
    caster, warrior
}

public abstract class EnemyInfo : MonoBehaviour
{
    EInfo eInfo;            //몬스터 정보 구조체 불러오기
    int type;               //몬스터 타입(스킬 캐스팅이 있는지 없는지)

    //적 정보 getset
    public int getHP(){return eInfo.HP;}
    public int getAttack(){return eInfo.attack;}
    public int getDefend(){return eInfo.defend;}
    public float getCastingTime(){return eInfo.castingTime;}
    public float getDistance(){return eInfo.distance;}
    public float getAttDistance(){return eInfo.attDistance;}

    public int getType() { return type; }
}
