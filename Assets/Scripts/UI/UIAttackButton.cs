using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseRPG_V1;

public class UIAttackButton : BaseObject
{
    public override void Initialization()
    {
    }

    public override void DisposeObject()
    {
    }

    // 터치 이벤트.
    public void OnTouchButton()
    {
        var player = PoolManager.Instance.GetObject<PlayerController>();

        if (player == null)
        {
            return;
        }

        // 공격.
        player.OnAttack();
    }
}
