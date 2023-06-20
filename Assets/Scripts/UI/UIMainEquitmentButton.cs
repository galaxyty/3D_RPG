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
        Debug.Log("장비창 페이지");
    }
}
