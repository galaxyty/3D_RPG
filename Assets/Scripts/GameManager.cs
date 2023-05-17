using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseRPG_V1;

public class GameManager : BaseSingleton<GameManager>
{
    private void Start()
    {
        // 유저 생성.
        PoolManager.Instance.Create<PlayerController>(Constants.kBUNDLE.Player.ToString());
        PoolManager.Instance.Pop<PlayerController>();

        // 몬스터 스폰 생성.
        PoolManager.Instance.Create<SpawnEnemy>(Constants.kBUNDLE.SpawnEnemy.ToString());
        PoolManager.Instance.Pop<SpawnEnemy>();

        // 유저 인터페이스 셋팅.
        PoolManager.Instance.Create<UICharacter>(Constants.kBUNDLE.UICharacter.ToString());
        PoolManager.Instance.Pop<UICharacter>(Constants.kTAG.MainCanvas.ToString());

        // 스킬매니저 셋팅.
        SkillManager.Instance.Initialization();
    }

    // GameManager 싱글톤 생성을 위한 함수. (Start 함수 실행을 위해 만들어둠).
    public void GameStart()
    {        
    }
}
