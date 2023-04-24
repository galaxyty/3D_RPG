using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseRPG_V1;

public class EfLevelUp : BaseObject
{
    // 이펙트 종료 시간.
    private float m_Count;

    public override void Initialization()
    {
        m_Count = 4.0f;
    }

    public override void DisposeObject()
    {
        ;
    }

    // 이펙트 종료 코루틴 함수.
    public void Count()
    {
        ResetLocalPosition();
        StartCoroutine(ECount());
    }

    // 일정 시간 후 코루틴 종료.
    private IEnumerator ECount()
    {
        yield return new WaitForSeconds(m_Count);
        PoolManager.Instance.Push(this);
    }
}
