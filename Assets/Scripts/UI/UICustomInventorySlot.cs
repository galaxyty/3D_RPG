using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BaseRPG_V1;

public class UICustomInventorySlot : BaseInventorySlot
{
    // 인덱스 텍스트 UI.
    [SerializeField]
    protected Text m_TextOfIndex;

    // 아이템 이미지.
    [SerializeField]
    protected Image m_ImageOfItem;

    // 아이템 소지 데이터.
    protected ItemData m_Data;

    public override void Initialization()
    {
        base.Initialization();

        m_ImageOfItem.sprite = BundleManager.Instance.LoadToSprite(Constants.kBUNDLE.UIInventorySlot.ToString());
        m_TextOfIndex.gameObject.SetActive(false);
    }

    public override void DisposeObject()
    {
        base.DisposeObject();

        m_TextOfIndex.gameObject.SetActive(false);
        m_TextOfIndex.text = null;
        m_ImageOfItem.sprite = BundleManager.Instance.LoadToSprite(Constants.kBUNDLE.UIInventorySlot.ToString());
        m_Data = null;
    }

    // 인벤토리 슬롯 업데이트.
    public void UpdateUI(ItemData data)
    {
        // 데이터 넣기.
        m_Data = data;

        // null 체크.
        if (m_Data == null)
            return;

        // ui 갱신.
        m_TextOfIndex.gameObject.SetActive(true);
        m_TextOfIndex.text = m_Data.INDEX.ToString();
        m_ImageOfItem.sprite = BundleManager.Instance.LoadToSprite(m_Data.INDEX.ToString());
    }

    // 포션 아이템.
    private void UsePorsion()
    {
        // 포션 아이템.
        var porsion = TableManager.Instance.GetPorsionData().Find(foundData => foundData.ITEM_INDEX == m_Data.INDEX);

        // null 체크.
        if (porsion == null)
            return;

        // 체력 회복.
        var player = PoolManager.Instance.GetObject<PlayerController>();

        // null 체크.
        if (player == null)
            return;

        // 체력 회복.
        player.CureHP(porsion.HP);

        // 사용한 아이템 제거.
        player.Inventory.Remove(m_Data);

        DisposeObject();
    }

    // 인벤토리 슬롯 버튼.
    public override void OnTouchEvent()
    {
        // null 체크.
        if (m_Data == null)
            return;

        switch (m_Data.ITEM_TYPE)
        {
            case ItemData.kITEM_TYPE.Weapon:
                break;

            case ItemData.kITEM_TYPE.Porsion:
                UsePorsion();
                break;

            case ItemData.kITEM_TYPE.Armor:
                break;
        }
    }
}
