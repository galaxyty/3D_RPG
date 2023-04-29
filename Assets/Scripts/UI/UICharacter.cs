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

    // 게이지바 배경.
    [SerializeField]
    private Image m_ImageOfInfo;

    // 레벨 배경.
    [SerializeField]
    private Image m_ImageOfLevelBG;
    
    // HP 외각선.
    [SerializeField]
    private Image m_ImageOfHPFrameBG;

    // HP 배경.
    [SerializeField]
    private Image m_ImageOfHPBG;

    // 경험치 외각선.
    [SerializeField]
    private Image m_ImageOfEXPFrameBG;

    // 경험치 배경.
    [SerializeField]
    private Image m_ImageOfEXPBG;

    public override void Initialization()
    {
        m_ImageOfInfo.sprite = BundleManager.Instance.LoadToSprite(Constants.kBUNDLE.UIInformation.ToString());
        m_ImageOfLevelBG.sprite = BundleManager.Instance.LoadToSprite(Constants.kBUNDLE.UILevel.ToString());
        m_ImageOfHPFrameBG.sprite = BundleManager.Instance.LoadToSprite(Constants.kBUNDLE.UIHPFrame.ToString());
        m_ImageOfHPBG.sprite = BundleManager.Instance.LoadToSprite(Constants.kBUNDLE.UIHP.ToString());
        m_ImageOfEXPFrameBG.sprite = BundleManager.Instance.LoadToSprite(Constants.kBUNDLE.UIHPFrame.ToString());
        m_ImageOfEXPBG.sprite = BundleManager.Instance.LoadToSprite(Constants.kBUNDLE.UIHP.ToString());

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
