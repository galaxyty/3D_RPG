using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseRPG_V1;

public class UICustomEquitmentSlot : BaseInventorySlot
{
    // 장비 소지 데이터. (new 키워드로 m_Data 숨김).
    protected new EquipmentData m_Data;

    public override void Initialization()
    {
        base.Initialization();

        m_ImageOfItem.sprite = BundleManager.Instance.LoadToSprite(Constants.kBUNDLE.UIInventorySlot.ToString());
    }

    public override void DisposeObject()
    {
        base.DisposeObject();

        m_ImageOfItem.sprite = BundleManager.Instance.LoadToSprite(Constants.kBUNDLE.UIInventorySlot.ToString());
    }

    // 장비창 슬롯 업데이트.
    public void UpdateUI(EquipmentData data)
    {
        // null 체크.
        if (data == null)
        {
            return;
        }

        m_Data = data;

        // 슬롯에 인덱스 표시.
        m_TextOfIndex.text = data.ITEM_INDEX.ToString();

        // 이미지 변경.
        m_ImageOfItem.sprite = BundleManager.Instance.LoadToSprite(m_Data.ITEM_INDEX.ToString());        
    }

    public override void OnTouchEvent()
    {
        
    }
}
