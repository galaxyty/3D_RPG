using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseRPG_V1;

public class UICustomInventory : BaseInventory
{
    // 인벤토리 슬롯.
    private List<UICustomInventorySlot> m_ListOfSlot = new List<UICustomInventorySlot>();

    public override void Initialization()
    {
        base.Initialization();

        m_ImageOfBG.sprite = BundleManager.Instance.LoadToSprite(Constants.kBUNDLE.UIInventory.ToString());

        // 인벤토리 슬롯 풀 생성.
        PoolManager.Instance.Create<UICustomInventorySlot>(Constants.kBUNDLE.UICustomInventorySlot.ToString(), m_Count);
    }

    public override void DisposeObject()
    {
        base.DisposeObject();

        // 인벤토리 슬롯 풀 반환.
        PoolManager.Instance.PushList(m_ListOfSlot);
    }

    // 인벤토리 업데이트.
    public void UpdateUI(List<ItemData> list)
    {
        // 인벤토리 슬롯 생성.
        for (int i = 0; i < m_Count; i++)
        {
            var obj = PoolManager.Instance.Pop<UICustomInventorySlot>(m_Grid.transform);
            m_ListOfSlot.Add(obj);

            if (list.Count <= i)
                continue;

            obj.UpdateUI(list[i]);
        }
    }
}
