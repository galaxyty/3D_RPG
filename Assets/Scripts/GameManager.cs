using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseRPG_V1;

public class GameManager : BaseSingleton<GameManager>
{
    private void Start() 
    {
        // 캐릭터 셋팅.
        PoolManager.Instance.Create<PlayerController>(Constants.kBUNDLE.Player.ToString());
        PoolManager.Instance.Pop<PlayerController>();

        PoolManager.Instance.Create<EnemyController>(Constants.kMONSTER.Enemy.ToString());
        PoolManager.Instance.Pop<EnemyController>();

        // 유저 인터페이스 셋팅.
        PoolManager.Instance.Create<UICharacter>(Constants.kBUNDLE.UICharacter.ToString());
        PoolManager.Instance.Pop<UICharacter>(Constants.kTAG.MainCanvas.ToString());
    }
}
