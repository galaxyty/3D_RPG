using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseRPG_V1;

public class SkillManager : BaseSingleton<SkillManager>
{
    // 스킬매니저 셋팅.
    public void Initialization()
    {
        PoolManager.Instance.Create<FireSlash>(Constants.kBUNDLE.EfFireSlash.ToString());
    }

    // 스킬 사용.
    public void Skill<T>() where T : BaseSkill
    {
        // 플레이어 자식으로 생성하기 위해 플레이어 가져온다.
        var player = PoolManager.Instance.GetObject<PlayerController>();

        if (player == null)
        {
            return;
        }

        // 스킬 오브젝트.
        var obj = PoolManager.Instance.GetObject<T>();

        // 스킬 이펙트가 존재하면 막음.
        if (obj != null)
        {
            return;
        }

        // 스킬 생성.
        var skill = PoolManager.Instance.Pop<T>(player.transform);

        // 스킬 발동 효과.
        skill.OnSkill();

        if (skill == null)
        {
            return;
        }

        // 스킬 생성 후 자식 관계에서 빠져나온다.
        skill.transform.SetParent(null);
    }
}
