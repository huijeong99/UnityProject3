using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    enum State
    {
        Idle, Walk, Run, Attack1, Attack2, Hit, Die
    }

    State state;
    Animator Anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Animation();
    }

    private void Animation()
    {
        switch (state)
        {
            case State.Idle:
                Anim.SetTrigger("Idle");
                break;
            case State.Walk:
                Anim.SetTrigger("Walk");
                break;
            case State.Run:
                Anim.SetTrigger("Run");
                break;
            case State.Attack1:
                Anim.SetTrigger("Attack1");
                break;
            case State.Attack2:
                Anim.SetTrigger("Attack2");
                break;
            case State.Hit:
                Anim.SetTrigger("Hit");
                break;
            case State.Die:
                Anim.SetTrigger("Die");
                break;
        }
    }
}
