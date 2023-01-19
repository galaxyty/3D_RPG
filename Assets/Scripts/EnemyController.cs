using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseRPG_V1;

public class EnemyController : BaseCharacter
{
    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.tag == "Player")
        {
            var obj = PoolManager.Instance.GetObject<PlayerController>();

            if (obj == null)
                return;

            Debug.Log("asdasd : " + obj.Hp);
        }
    }
}
