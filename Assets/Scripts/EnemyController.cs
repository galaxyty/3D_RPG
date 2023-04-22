using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseRPG_V1;

public class EnemyController : BaseCharacter
{
    // 인덱스.
    private int m_Index;

    // 인덱스 프로퍼티.
    public int Index
    {
        get
        {
            return m_Index;
        }
        set
        {
            m_Index = value;
        }
    }

    // 스폰 객체.
    public SpawnEnemy spawnEnemy = null;

    // 풀매니저로 돌아갈 시.
    public override void DisposeObject()
    {
        base.DisposeObject();

        spawnEnemy.StartSpawn();
        spawnEnemy = null;        
    }

    public override void Hit(int damage)
    {
        base.Hit(damage);

        Debug.Log("남은 HP : " + m_Hp);
    }

    // 체력 소모로 사망 시.
    public override void Die()
    {
        base.Die();
        
        var monster = TableManager.Instance.GetMonsterData().Find(foundData => foundData.INDEX == m_Index);
        var player = PoolManager.Instance.GetObject<PlayerController>();

        if (monster == null || player == null)
        {
            return;
        }

        player.SetEXP(monster.EXP);

        PoolManager.Instance.GetObject<UICharacter>().UpdateLevelUI();

        PoolManager.Instance.Push(this);
    }

    private void OnTriggerEnter(Collider other) 
    {
        // 받은 데미지.
        int damage = 0;

        // 플레이어한테 피격당할 시.
        if (other.gameObject.tag == Constants.kTAG.Weapon.ToString())
        {
            var player = other.transform.root.GetComponent<PlayerController>();

            // null 체크.
            if (player == null)
                return;
            
            damage = player.AttackPower;
        }

        Hit(damage);
    }
}
