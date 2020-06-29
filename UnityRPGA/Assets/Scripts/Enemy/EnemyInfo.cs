using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyInfo : MonoBehaviour
{
    public abstract EnemyInfo Clone();
}

public class DragonInfo : EnemyInfo
{
    public override EnemyInfo Clone()
    {
        return (DragonInfo)this.MemberwiseClone();
        //MemberwiseClone을 사용할 시 별도의 생성자를 통한 인스턴스를 생성하는 것이 아닌
        //기존의 객체를 복사하게 된다
    }
}

public class EnemyFactory : MonoBehaviour
{
    private static List<EnemyInfo> enemyInfo = new List<EnemyInfo>();

    public EnemyFactory()
    {
        enemyInfo.Add(new DragonInfo());
    }
}