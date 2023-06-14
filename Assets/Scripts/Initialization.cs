using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BaseRPG_V1;

public class Initialization : MonoBehaviour
{
    // 번들 다운로드 패널.
    [SerializeField]
    private GameObject m_ObjectOfPanel;

    // 번들 다운로드 백그라운드.
    [SerializeField]
    private GameObject m_ObjectOfBG;

    [SerializeField]
    private Slider m_SliderOfBundle;

    // 게임 실행 시 최초 실행.
    private void Awake()
    {
        m_ObjectOfPanel.SetActive(false);
        BundleDownloadManager.Instance.DownloadCheck(Constants.kBUNDLE.Player.ToString(), BundleFirstCheck, BundleAlready);
    }

    // 최초 번들 다운로드 여부 확인.
    private void BundleFirstCheck()
    {
        m_ObjectOfPanel.SetActive(true);
        m_ObjectOfBG.SetActive(true);
        m_SliderOfBundle.gameObject.SetActive(false);
    }

    // 번들 다운로드 함수.
    private void BundleDownload()
    {
        m_ObjectOfBG.SetActive(false);
        m_SliderOfBundle.gameObject.SetActive(true);
        BundleDownloadManager.Instance.DownloadBundleAsync(Constants.kBUNDLE.Player.ToString(), BundleAlready);
    }
    
    // 번들 다운로드 완료 후 실행 함수.
    private void BundleAlready()
    {
        m_ObjectOfPanel.SetActive(false);
        GameManager.Instance.GameStart();        
    }

    // 번들 다운로드하는 버튼.
    public void OnTouchOK()
    {
        BundleDownload();
    }

    private void Update() 
    {
        m_SliderOfBundle.value = BundleDownloadManager.Instance.Percent;
    }
}
