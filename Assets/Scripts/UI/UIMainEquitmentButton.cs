using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseRPG_V1;
using UnityEngine.UI;

public class UIMainEquitmentButton : BaseObject
{
    [SerializeField]
    private Image m_ImageOfBG;

    public override void Initialization()
    {
        m_ImageOfBG.sprite = BundleManager.Instance.LoadToSprite(Constants.kBUNDLE.UIMainEquitment.ToString());
    }

    public override void DisposeObject()
    {

    }

    public void OnTouchEvent()
    {
        // 아이템 랜덤 생성.
        ItemData item = TableManager.Instance.GetItemData()[Random.Range(0, TableManager.Instance.GetItemData().Count)];
        FirebaseManager.Instance.JsonToItemData(item);

        var obj = PopupManager.Instance.Open<UICustomEquitment>(Constants.kTAG.MainCanvas.ToString());

        if (obj == null)
        {
            return;
        }

        obj.UpdateUI();
    }
}
