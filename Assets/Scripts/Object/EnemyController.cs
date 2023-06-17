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

        var player = PoolManager.Instance.GetObject<PlayerController>();

        if (player == null)
        {
            return;
        }

        player.SetEXP(m_Exp);

        PoolManager.Instance.GetObject<UICharacter>().UpdateLevelUI();

        PoolManager.Instance.Push(this);
    }

    // 능력치 셋팅.
    public void SetStatus(MonsterData monster)
    {
        m_Index = monster.INDEX;
        m_Hp = monster.HP;
        m_MaxHp = monster.HP;
        m_Exp = monster.EXP;
    }

    private void OnTriggerEnter(Collider other) 
    {
        // 받은 데미지.
        int damage = 0;

        // 플레이어한테 피격당할 시.
        if (other.gameObject.CompareTag(Constants.kTAG.Weapon.ToString()))
        {
            var player = other.transform.root.GetComponent<PlayerController>();

            // null 체크.
            if (player == null)
                return;
            
            damage = player.AttackPower;            
        }

        // 스킬 맞을 시.
        var skill = other.GetComponent<BaseSkill>();
        if (skill != null)
        {
            skill.OnHit(ref damage);
        }

        Hit(damage);
    }
}
