using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseRPG_V1;

public class UIMainMenuButton : BaseObject
{
    public override void Initialization()
    {
    }

    public override void DisposeObject()
    {
    }

    public void OnTouchEvent()
    {
        // 필드 생성.
        FieldStart();

        // 파베 초기화.
        FirebaseManager.Instance.Initialization();
    }

    // 필드에 캐릭터 생성.
    private void FieldStart()
    {
        // 메인화면 제거.
        PoolManager.Instance.Push(this);
        PoolManager.Instance.Push(PoolManager.Instance.GetObject<MainMenu>());

        // 스킬매니저 셋팅.
        SkillManager.Instance.Initialization();

        // 유저 생성.
        PoolManager.Instance.Create<PlayerController>(Constants.kBUNDLE.Player.ToString());
        PoolManager.Instance.Pop<PlayerController>();

        // 몬스터 스폰 생성.
        PoolManager.Instance.Create<SpawnEnemy>(Constants.kBUNDLE.SpawnEnemy.ToString());
        PoolManager.Instance.Pop<SpawnEnemy>();

        // 유저 인터페이스 셋팅.
        PoolManager.Instance.Create<UICharacter>(Constants.kBUNDLE.UICharacter.ToString());
        PoolManager.Instance.Pop<UICharacter>(Constants.kTAG.MainCanvas.ToString());
    }
}
