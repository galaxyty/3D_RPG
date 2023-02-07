using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseRPG_V1;

public class EnemyController : BaseCharacter
{
    public override void Hit(int damage)
    {
        base.Hit(damage);

        Debug.Log("남은 HP : " + m_Hp);
    }

    public override void Die()
    {
        base.Die();
        
        Debug.Log("사망");

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
