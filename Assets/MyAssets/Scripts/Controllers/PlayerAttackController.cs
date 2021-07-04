using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �U���{�^�����A�v���C���[�ɍU��������R���|�[�l���g
/// </summary>
public class PlayerAttackController : MonoBehaviour
{
    [Header("inputmanager��̃{�^����")]
    
    [SerializeField, Tooltip("��U���{�^����")] string button_commonAttack = "Fire1";
    [SerializeField, Tooltip("���U���{�^����")] string button_strongAttack = "Fire2";

    [Space]

    /// <summary>
    /// ����(�f��)���
    /// </summary>
    WeaponInfo[] weapons = default;

    /// <summary>
    /// �f��̕��햼
    /// </summary>
    [SerializeField, Tooltip("�f��̏ꍇ�̕��햼")] string weapon_bareHand = "�f��";

    /// <summary>
    /// ��U���̎��{��v��
    /// </summary>
    bool doCommonAttack = false;
    /// <summary>
    /// ���U���̎��{��v��
    /// </summary>
    bool doStrongAttack = false;

    /// <summary>
    /// ����𑕔����ł���
    /// </summary>7
    bool isArmed = false;

    /// <summary>
    /// �U���͈͂��L���ł���(ForPlayerGladiatorAnimator����ύX)
    /// </summary>
    bool isAttacking = false;

    /// <summary>
    /// ���̃A�N�V�����̎�t�������Ă��邩(ForPlayerGladiatorAnimator����ύX)
    /// </summary>
    bool isAcceptOtherActions = true;



    public bool DoCommonAttack { get => doCommonAttack; set => doCommonAttack = value; }
    public bool DoStrongAttack { get => doStrongAttack; set => doStrongAttack = value; }
    public bool IsArmed { get => isArmed; set => isArmed = value; }
    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }
    public bool IsAcceptOtherActions { get => isAcceptOtherActions; set => isAcceptOtherActions = value; }




    // Start is called before the first frame update
    void Start()
    {
        weapons = GetComponentsInChildren<WeaponInfo>();
        CheckArmed();
    }

    // Update is called once per frame
    void Update()
    {
        //�U���t���O������
        doCommonAttack = false;
        doStrongAttack = false;
        //�U���A�j���[�V�������U�������I���Ă���̂ŁA���̑�����͂������Ă���
        if (isAcceptOtherActions)
        {
            if (Input.GetButtonDown(button_commonAttack))
            {
                doCommonAttack = true;
            }
            else if (Input.GetButtonDown(button_strongAttack))
            {
                doStrongAttack = true;
            }
        }
        


        //�U���A�j���[�V�������A�_���[�W����̂��铮���ɓ����Ă���
        if (isAttacking)
        {
            //���했�ɓ����蔻��R���C�_�[���N��
            foreach (WeaponInfo wep in weapons)
            {
                if (isArmed || wep.Name == weapon_bareHand)
                {
                    wep.RangeActivator(true);
                }
            }
        }
        else
        {
            //���했�ɓ����蔻��R���C�_�[���I��
            foreach (WeaponInfo wep in weapons)
            {
                wep.RangeActivator(false);
            }
        }
    }

    /// <summary>
    /// ����𑕔������`�F�b�N
    /// </summary>
    void CheckArmed()
    {
        isArmed = false;
        foreach (WeaponInfo wep in weapons)
        {
            if (wep.Name == weapon_bareHand) continue;

            isArmed = true;
            break;
        }
    }
}
