using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public float followSpeed = 0.05f;

    public float distance=10.0f;
    public float height = 4.0f;
    public float rotate = 10.0f;

    // Start is called before the first frame update
    void Start()
    { }

    // Update is called once per frame
    void LateUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        var CamPos = target.transform.position-(target.transform.forward*distance)+(target.transform.up*height);

        transform.position = Vector3.Slerp(transform.position, CamPos, Time.deltaTime * rotate);

        transform.LookAt(target.transform.position+(target.transform.up));
    }
}
