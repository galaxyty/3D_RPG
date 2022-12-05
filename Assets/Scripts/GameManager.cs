using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseRPG_V1;

public class GameManager : BaseSingleton<GameManager>
{
    private void Start() 
    {
        // 테이블 매니저 초기화.
        TableManager.Instantce.Init();        
    }
}
