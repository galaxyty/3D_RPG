using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseRPG_V1;
using UnityEngine.UI;

public class UIMainInventoryButton : BaseObject
{
    [SerializeField]
    private Image m_ImageOfBG;

    public override void Initialization()
    {
        m_ImageOfBG.sprite = BundleManager.Instance.LoadToSprite(Constants.kBUNDLE.UIMainInventory.ToString());
    }

    public override void DisposeObject()
    {

    }

    public void OnTouchEvent()
    {
        Debug.Log("인벤토리 페이지");
    }
}
