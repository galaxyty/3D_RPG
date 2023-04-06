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
    }

    // 스폰 객체.
    public SpawnEnemy spawnEnemy = null;

    public override void Hit(int damage)
    {
        base.Hit(damage);

        Debug.Log("남은 HP : " + m_Hp);
    }

    public override void Die()
    {
        base.Die();
        
        Debug.Log("사망");
        spawnEnemy.StartSpawn();
        spawnEnemy = null;

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
