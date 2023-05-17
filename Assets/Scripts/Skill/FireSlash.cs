using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseRPG_V1;

public class FireSlash : BaseSkill
{
    // 첫번째 파티클.
    [SerializeField]
    private ParticleSystem m_PsFlash;

    // 두번째 파티클.
    [SerializeField]
    private ParticleSystem m_PsSmoke;    

    public override void Initialization()
    {
        base.Initialization();
    }

    public override void DisposeObject()
    {
        base.DisposeObject();

        m_PsFlash.Pause();
        m_PsSmoke.Pause();
    }

    public override void OnHit(ref int damage)
    {
        damage += m_Attack;
    }

    public override void OnSkill()
    {
        m_PsFlash.Play();
        m_PsSmoke.Play();

        StartCoroutine(EEffect_Count());
    }
}