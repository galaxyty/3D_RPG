using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseRPG_V1;

public class SpawnEnemy : BaseSpawn
{
    // 스폰시킬 몬스터 풀매니저 크리에이트.
    public override void SpawnCreate()
    {
        PoolManager.Instance.Create<EnemyController>(Constants.kMONSTER.Enemy.ToString());
    }

    // 일정시간마다 크리에이트시킨 몬스터 필드에 생성.
    public override void SpawnPop()
    {
        var obj = PoolManager.Instance.Pop<EnemyController>(transform);
        obj.spawnEnemy = this;
        obj.Index = TableManager.Instance.GetMonsterData().Find(foundData => foundData.INDEX == m_SpawnIndex).INDEX;
        m_currentCount++;
    }
}
