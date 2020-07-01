using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpon : MonoBehaviour
{
    //PlayerCtrl playerInfo;             //플레이어 정보를 가져오기 위한 선언(제대로 안되는듯함)
    public List<GameObject> enemy;     //스폰될 적의 모델을 담을 리스트
    int index;
    float sponTime;                    //스폰간격 조정
    float curTime = 0;                 //스폰 타이머
    int maxEnemy;                      //스폰될 에너미의 최대 개체수
    int curEnemy = 0;                  //스폰된 에너미 세기

    // Start is called before the first frame update
    // PlayerCtrl에서 Awake에서 처리해준 이후 처리해줘야 하기 때문에 Start에 선언
    private void Start()
    {
        sponTime = 500.0f;  //임시로 최대 레벨을 10으로 잡음
        //maxEnemy = playerInfo.playerLvSetting() * 2;    //왜안될까
        maxEnemy = 5;    //임시로 5마리로 지정
    }

    // Update is called once per frame
    void Update()
    {
        sponEnemy();
    }

    private void sponEnemy()
    {
        if (curTime%sponTime==0 && curEnemy < maxEnemy)
        {
            index = UnityEngine.Random.Range(0, enemy.Count);//적의 모델 리스트 수 내에서 랜덤으로 인덱스 하나를 선출함
            Instantiate(enemy[index], this.transform);        //해당 위치에 선택된 적을 생성함
            curEnemy++;                                      //소환된 적 수 늘리기
            //curTime = 0;                                     //타이머 초기화
        }

        curTime += 1.0f;
    }
}
