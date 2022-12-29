using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseRPG_V1;

public class GameManager : BaseSingleton<GameManager>
{
    private void Start() 
    {
        // 번들 다운로드.        
        BundleManager.Instance.DownloadBundleAsync();
        
        PoolManager.Instance.Create<PlayerController>(Constants.kBUNDLE.PLAYER.ToString());
        PoolManager.Instance.Pop<PlayerController>();
    }
}
