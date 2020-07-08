using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPlayer : MonoBehaviour
{
    [SerializeField] Transform player;
    Quaternion StartRot;
    [SerializeField] float distance = 2.0f;   //플레이어 인식범위
    float speed = 5.0f;                       //회전속도

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        StartRot =transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        RotateToSP();
    }

    private void RotateToSP()
    {
        //플레이어 방향을 향해 회전
        if (Vector3.Distance(transform.position, player.transform.position) < distance)
        {
            Vector3 targetDir = player.position - transform.position;
            float step = speed * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }

        //멀어지면 처음 방향으로 다시 회전
        else
        {
            //Quaternion rotate = Quaternion.Euler(0, 0, 0);  //되돌릴 회전각
            Quaternion rotate = StartRot;  //되돌릴 회전각

            transform.rotation = Quaternion.Lerp(transform.rotation, rotate, Time.deltaTime * 3.0f);
        }
    }
}
