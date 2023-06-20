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
        var obj = PopupManager.Instance.Open<UICustomInventory>(Constants.kTAG.MainCanvas.ToString());

        if (obj == null)
        {
            return;
        }

        obj.UpdateUI(FirebaseManager.Instance.UserData.ITEM);

        // 테스트옹 파이어볼 발사.
        SkillManager.Instance.Skill<FireSlash>();
    }
}
