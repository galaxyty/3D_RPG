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

    public override void Initialization()
    {
        UpdateLevelUI();
    }

    public override void DisposeObject()
    {
        
    }

    // 레벨 UI 업데이트.
    private void UpdateLevelUI()
    {
        var player = PoolManager.Instance.GetObject<PlayerController>();

        if (player == null)
        {
            return;
        }

        m_TextOfLevel.text = player.Level.ToString();
    }
}
