using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseRPG_V1;
using UnityEngine.UI;

public class UICharacter : BaseObject
{
    // 레벨 텍스트.
    [SerializeField]
    private Text m_TextOfLevel;

    // 체력바.
    [SerializeField]
    private Slider m_SliderOfHP;

    // 경험치바.
    [SerializeField]
    private Slider m_SliderOfExp;

    public override void Initialization()
    {
        UpdateLevelUI();
        UpdateHpUI();
    }

    public override void DisposeObject()
    {
        
    }

    // 레벨 UI 업데이트.
    public void UpdateLevelUI()
    {
        var player = PoolManager.Instance.GetObject<PlayerController>();

        if (player == null)
        {
            return;
        }

        m_TextOfLevel.text = player.Level.ToString();

        m_SliderOfExp.maxValue = player.MaxExp;
        m_SliderOfExp.value = player.Exp;
    }

    // 체력 UI 업데이트.
    public void UpdateHpUI()
    {
        var player = PoolManager.Instance.GetObject<PlayerController>();

        if (player == null)
        {
            return;
        }

        m_SliderOfHP.maxValue = player.MaxHp;
        m_SliderOfHP.value = player.Hp;
    }
}
