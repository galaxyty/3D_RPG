using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseRPG_V1;

public class GameManager : BaseSingleton<GameManager>
{
    private void Start() 
    {   
        PoolManager.Instance.Create<PlayerController>(Constants.kBUNDLE.PLAYER.ToString());
        PoolManager.Instance.Pop<PlayerController>();
    }
}
