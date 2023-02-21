using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICustomInventorySlot : UIInventorySlot
{
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

    // 인벤토리 슬롯 버튼.
    public override void OnTouchEvent()
    {
        // null 체크.
        if (m_Data == null)
            return;

        // 플레이어 객체 가져옴.
        var player = PoolManager.Instance.GetObject<PlayerController>();

        switch (m_Data.ITEM_TYPE)
        {
            case ItemData.kITEM_TYPE.Weapon:

                // null 체크.
                if (player == null)
                    return;

                // 무기 장착.
                player.UpdateWeapon(m_Data);

                break;

            case ItemData.kITEM_TYPE.Porsion:
            
                // 포션 사용.
                UsePorsion();
                break;

            case ItemData.kITEM_TYPE.Armor:

                // null 체크.
                if (player == null)
                    return;

                // 방어구 장착.
                player.UpdateArmor(m_Data);                

                break;
        }

        // 장비창 열려있는지?.
        var equit = PoolManager.Instance.GetObject<UIEquitment>();

        // 장비창 열려있으면 실시간 변경.
        if (equit != null)
        {
            equit.UpdateUI();
        }
    }
}
