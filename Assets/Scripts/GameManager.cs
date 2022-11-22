using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseRPG_V1;

public class GameManager : BaseSingleton<GameManager>
{
    private void Start() 
    {
        Test TEST = JsonManager.Instantce.Parse<Test>("TEST");

        Debug.Log(TEST.ID);
    }
}
