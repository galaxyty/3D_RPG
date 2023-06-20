using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseRPG_V1;

public class PlayerController : BasePlayerCharacter
{
    private kMOVE m_Move = kMOVE.None;

    // 애니메이터.
    [SerializeField]
    private Animator m_Animator;

    // 공격용 메쉬콜라이더.
    [SerializeField]
    private MeshCollider m_Mesh;

    // 공격중인가?.
    private bool m_Attack = false;

    // 레벨 데이터.
    private List<LevelData> levelData = new List<LevelData>();

    // 조이스틱.
    private VariableJoystick m_Joystick = null;

    private void Update() 
    {
        LeftClick();
    }

    private void FixedUpdate() 
    {
        JoystickMove(m_Joystick.Horizontal, m_Joystick.Vertical, () =>
        {
            // 움직이는 애니메이션 재생을 위해 타입 변경.
            m_Move = kMOVE.Forward;
        });        
        
        Move();
    }

    // 좌클릭.
    public override void OnLeftClick()
    {
    }

    // 공격.
    public void OnAttack()
    {
        if (m_Attack == true)
            return;

        m_Move = kMOVE.Attack;
        StartCoroutine(EAttack());
    }

    public override void Move()
    {
        base.Move();

        // 애니메이션 재생.
        m_Animator.SetInteger("Movement", (int)m_Move);

        // 애니메이션 원래대로 돌아옴.
        m_Move = kMOVE.None;
    }

    public override void Initialization()
    {
        base.Initialization();        

        m_Mesh.enabled = false;

        // 인벤토리 및 장비창 풀 셋팅.
        PoolManager.Instance.Create<UICustomInventory>(Constants.kBUNDLE.UICustomInventory.ToString());
        PoolManager.Instance.Create<UICustomEquitment>(Constants.kBUNDLE.UICustomEquitment.ToString());

        // 레벨업 이펙트 셋팅.
        PoolManager.Instance.Create<EfLevelUp>(Constants.kBUNDLE.EfLevelUp.ToString());

        // 조이스틱.
        PoolManager.Instance.Create<VariableJoystick>(Constants.kBUNDLE.Joystick.ToString());
        m_Joystick = PoolManager.Instance.Pop<VariableJoystick>(Constants.kTAG.MainCanvas.ToString());

        // 공격 버튼.
        PoolManager.Instance.Create<UIAttackButton>(Constants.kBUNDLE.UIAttackButton.ToString());
        PoolManager.Instance.Pop<UIAttackButton>(Constants.kTAG.MainCanvas.ToString());

        PoolManager.Instance.Create<UIMainInventoryButton>(Constants.kBUNDLE.UIMainInventoryButton.ToString());
        PoolManager.Instance.Pop<UIMainInventoryButton>(Constants.kTAG.MainCanvas.ToString());

        PoolManager.Instance.Create<UIMainEquitmentButton>(Constants.kBUNDLE.UIMainEquitmentButton.ToString());
        PoolManager.Instance.Pop<UIMainEquitmentButton>(Constants.kTAG.MainCanvas.ToString());

        PoolManager.Instance.Create<UIMainSkillButton>(Constants.kBUNDLE.UIMainSkillButton.ToString());
        PoolManager.Instance.Pop<UIMainSkillButton>(Constants.kTAG.MainCanvas.ToString());        

        // 카메라.
        PoolManager.Instance.Create<CameraController>(Constants.kBUNDLE.PlayerCamera.ToString());
        PoolManager.Instance.Pop<CameraController>().SettingTarget(gameObject);

        m_Hp = 40;
        m_MaxHp = 40;
        m_Level = 1;
        m_Exp = 0;

        levelData = TableManager.Instance.GetLevelData();

        m_MaxExp = levelData.Find(foundData => foundData.LV == m_Level).EXP;
    }

    public override void DisposeObject()
    {
        base.DisposeObject();        
    }

    // 사망 이벤트 정의.
    public override void Die()
    {
        Debug.Log("사망");
        base.Die();
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
            
            case "Enemy":
                Hit(5);
                break;
            
            default:
                break;
        }
    }

    // 경험치 획득 함수.
    public override void SetEXP(int exp)
    {
        // null 체크.
        if (levelData == null)
        {
            return;
        }

        // 만렙이면 함수 종료.
        if (levelData[levelData.Count - 1].LV == m_Level)
        {
            return;
        }

        base.SetEXP(exp);
        
        // 해당 레벨에 맞는 데이터 가져온다.
        var data = levelData.Find(foundData => foundData.LV == m_Level);

        // null 체크.
        if (data == null)
        {
            return;
        }        

        // 레벨업 후 목표경험치 재설정.
        m_MaxExp = data.EXP;
    }

    // 레벨업 함수.
    public override void LevelUP()
    {
        base.LevelUP();

        // 이펙트 활성화.
        var obj = PoolManager.Instance.Pop<EfLevelUp>(transform);
        obj.Count();
    }

    // 피격 시 (공격 받을 시).
    public override void Hit(int damage)
    {
        base.Hit(damage);

        // 체력 UI 가져온다.
        var ui = PoolManager.Instance.GetObject<UICharacter>();

        if (ui == null)
        {
            return;
        }

        ui.UpdateHpUI();
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
    }

    // 공격 코루틴.
    private IEnumerator EAttack()
    {
        m_Attack = true;

        yield return new WaitForSeconds(0.1f);
        m_Mesh.enabled = true;

        yield return new WaitForSeconds(0.2f);
        m_Mesh.enabled = false;

        yield return new WaitForSeconds(0.2f);
        m_Attack = false;
    }
}
