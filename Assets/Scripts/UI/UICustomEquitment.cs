using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseRPG_V1;

public class UICustomEquitment : BaseInventory
{
    // 장비창 슬롯.
    private List<UICustomEquitmentSlot> m_ListOfSlot = new List<UICustomEquitmentSlot>();

    public override void Initialization()
    {
        base.Initialization();

        m_ImageOfBG.sprite = BundleManager.Instance.LoadToSprite(Constants.kBUNDLE.UIInventory.ToString());

        // 장비창 슬롯 풀 생성.
        PoolManager.Instance.Create<UICustomEquitmentSlot>(Constants.kBUNDLE.UICustomEquitmentSlot.ToString(), m_Count);
    }

    public override void DisposeObject()
    {
        base.DisposeObject();

        // 장비창 슬롯 풀 반환.
        PoolManager.Instance.PushList(m_ListOfSlot);
    }

    // 장비창 업데이트.
    public void UpdateUI()
    {
        if (m_ListOfSlot != null)
        {
            PoolManager.Instance.PushList(m_ListOfSlot);
        }

        var player = PoolManager.Instance.GetObject<PlayerController>();

        // m_ListOfSlot장비창 슬롯 생성.
        for (int i = 0; i < m_Count; i++)
        {
            var obj = PoolManager.Instance.Pop<UICustomEquitmentSlot>(m_Grid.transform);
            m_ListOfSlot.Add(obj);

            // null 체크.
            if (player == null)
                return;

            // 해당 장비 슬롯은 어떤 타입인지 결정.
            switch (i)
            {
                case (int)ItemData.kITEM_TYPE.Weapon:
                    obj.UpdateUI(player.Weapon);
                    break;

                case (int)ItemData.kITEM_TYPE.Armor:
                    obj.UpdateUI(player.Armor);
                    break;

                default:
                    break;
            }
        }
    }
}
