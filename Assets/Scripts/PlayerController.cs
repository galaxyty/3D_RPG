using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseRPG_V1;

public class PlayerController : BasePlayerCharacter
{   
    private enum kMOVE
    {
        None = 0,
        Forward,
        Back,
        Left,
        Right,
        ForwardLeft,
        ForwardRight,
        BackLeft,
        BackRight
    }

    private kMOVE m_Move = kMOVE.None;

    // 애니메이터.
    [SerializeField]
    private Animator m_Animator;

    private void Update() 
    {
        ThreeView();
        Move();
    }

    public override void Move()
    {
        base.Move();

        m_Animator.SetInteger("Movement", (int)m_Move);
    }    

    public override void Initialization()
    {
        base.Initialization();
        Debug.Log("풀 생성!");        
    }

    public override void DisposeObject()
    {
        base.DisposeObject();
        Debug.Log("풀 돌려보냄.");
    }

    // 사망 이벤트 정의.
    public override void Die()
    {
        base.Die();
        Debug.Log("사망");
    }

    // 피격.
    private void OnCollisionEnter(Collision other) 
    {
        var tag = other.gameObject.tag;

        switch (tag)
        {
            case "sad":
                Hit(10);
                break;
            
            case "tt":
                Hit(20);
                break;
            
            default:
                break;
        }
    }


    // W 키 입력 시.
    public override void W_Key()
    {
        base.W_Key();

        m_Move = kMOVE.Forward;
    }

    // W 키 취소.
    public override void W_KeyUp()
    {
        base.W_KeyUp();        

        m_Move = kMOVE.None;        
        Debug.Log(TableManager.Instance.GetItemData().ITEM_NAME);
    }

    // S 키 입력 시.
    public override void S_Key()
    {
        base.S_Key();

        m_Move = kMOVE.Back;
    }

    // S 키 취소.
    public override void S_KeyUp()
    {
        base.S_KeyUp();

        m_Move = kMOVE.None;
    }

    // A 키 입력 시.
    public override void A_Key()
    {
        base.A_Key();

        // 대각선 왼쪽.
        if (m_Move == kMOVE.Forward)
        {
            m_Move = kMOVE.ForwardLeft;
            return;
        }

        if (m_Move == kMOVE.Back)
        {
            m_Move = kMOVE.BackLeft;
            return;
        }
        
        m_Move = kMOVE.Left;
    }

    // A 키 취소.
    public override void A_KeyUp()
    {
        base.A_KeyUp();

        m_Move = kMOVE.None;
    }

    // D 키 입력 시.
    public override void D_Key()
    {
        base.D_Key();

        // 대각선 오른쪽.
        if (m_Move == kMOVE.Forward)
        {
            m_Move = kMOVE.ForwardRight;
            return;
        }

        if (m_Move == kMOVE.Back)
        {
            m_Move = kMOVE.BackRight;
            return;
        }

        m_Move = kMOVE.Right;
    }

    // D 키 취소.
    public override void D_KeyUp()
    {
        base.D_KeyUp();
        
        m_Move = kMOVE.None;
    }
}
