using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseRPG_V1;

public class EnemyController : BaseCharacter
{
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == Constants.kTAG.Weapon.ToString())
        {
            var obj = PoolManager.Instance.GetObject<PlayerController>();

            if (obj == null)
                return;
        }
    }
}
