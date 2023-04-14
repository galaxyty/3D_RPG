using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseRPG_V1;

public class GameManager : BaseSingleton<GameManager>
{
    private void Start() {
        BundleDownloadManager.Instance.DownloadBundleAsync(Constants.kBUNDLE.Player.ToString(), GameStart);
    }

    // 게임 시작 함수.
    public void GameStart()
    {
        // 캐릭터 셋팅.
        PoolManager.Instance.Create<PlayerController>(Constants.kBUNDLE.Player.ToString());
        PoolManager.Instance.Pop<PlayerController>();

        // 유저 인터페이스 셋팅.
        PoolManager.Instance.Create<UICharacter>(Constants.kBUNDLE.UICharacter.ToString());
        PoolManager.Instance.Pop<UICharacter>(Constants.kTAG.MainCanvas.ToString());
    }
}
