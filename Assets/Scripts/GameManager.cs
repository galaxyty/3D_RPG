using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseRPG_V1;

public class GameManager : BaseSingleton<GameManager>
{
    private void Start()
    {
        // 필드 생성.
        FieldStart();        
    }

    // GameManager 싱글톤 생성을 위한 함수. (Start 함수 실행을 위해 만들어둠).
    public void GameStart()
    {        
    }

    // 필드에 캐릭터 생성.
    private void FieldStart()
    {     
        // 메인 메뉴 버튼 생성.
        PoolManager.Instance.Create<UIMainMenuButton>(Constants.kBUNDLE.UIMainMenuButton.ToString());        
        PoolManager.Instance.Pop<UIMainMenuButton>(Constants.kTAG.MainCanvas.ToString());

        // 메인 화면 집 생성.
        PoolManager.Instance.Create<MainMenu>(Constants.kBUNDLE.MainMenu.ToString());
        PoolManager.Instance.Pop<MainMenu>();
    }
}
