using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class setPlayer : MonoBehaviour
{
    public abstract float getHP();
    public abstract int getlv();
    public abstract int getmaxExp();
    public abstract int getcurrExp();
    public abstract int getattack();
    public abstract int getdefend();
    public abstract float getsp();
}

public class Knight : setPlayer
{
    public override float getHP()
    {
        return 100;
    }

    public override int getlv()
    {
        return 1;
    }

    public override int getmaxExp()
    {
        return getlv() * 100;
    }

    public override int getcurrExp()
    {
        return 0;
    }

    public override int getattack()
    {
        return 10;
    }

    public override int getdefend()
    {
        return 10;
    }

    public override float getsp()
    {
        return 7.0f;
    }
}

public class Magician : setPlayer
{
    public override float getHP()
    {
        return 80;
    }

    public override int getlv()
    {
        return 1;
    }

    public override int getmaxExp()
    {
        return getlv() * 100;
    }

    public override int getcurrExp()
    {
        return 0;
    }

    public override int getattack()
    {
        return 20;
    }

    public override int getdefend()
    {
        return 5;
    }

    public override float getsp()
    {
        return 3.0f;
    }
}

public class Priest : setPlayer
{
    public override float getHP()
    {
        return 70;
    }

    public override int getlv()
    {
        return 1;
    }

    public override int getmaxExp()
    {
        return getlv() * 100;
    }

    public override int getcurrExp()
    {
        return 0;
    }

    public override int getattack()
    {
        return 5;
    }

    public override int getdefend()
    {
        return 20;
    }

    public override float getsp()
    {
        return 5.0f;
    }
}