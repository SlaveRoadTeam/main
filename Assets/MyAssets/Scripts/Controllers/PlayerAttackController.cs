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
    /// �L�����N�^�[�̔\�͒l
    /// </summary>
    CharacterStatus status = default;


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
    /// </summary>
    bool isArmed = false;
    /// <summary>
    /// �U���͈͂��L���ł���(ForPlayerGladiatorAnimator����ύX)
    /// </summary>
    bool isAttacking = false;
    /// <summary>
    /// ���̃A�N�V�����̎�t�������Ă��邩(ForPlayerGladiatorAnimator����ύX)
    /// </summary>
    bool isAcceptOtherActions = true;



    public bool DoCommonAttack { get => doCommonAttack; }
    public bool DoStrongAttack { get => doStrongAttack; }
    public bool IsArmed { get => isArmed; }
    public bool IsAttacking { set => isAttacking = value; }
    public bool IsAcceptOtherActions { set => isAcceptOtherActions = value; }




    // Start is called before the first frame update
    void Start()
    {
        weapons = GetComponentsInChildren<WeaponInfo>();
        status = GetComponentInChildren<CharacterStatus>();
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

            //����ɁA���U���ł��邩�̏��ƈЗ͕␳����n��
            foreach(WeaponInfo wep in weapons)
            {
                wep.DoStrongAttack = doStrongAttack;

                wep.PowerRatio = 1.0f;
                if (doStrongAttack) wep.PowerRatio = 1.5f;
            }
        }
        


        //�U���A�j���[�V�������A�_���[�W����̂��铮���ɓ����Ă���
        if (isAttacking)
        {
            //���했�ɓ����蔻��R���C�_�[���N��
            foreach (WeaponInfo wep in weapons)
            {
                if (isArmed)
                {
                    if(wep.WeaponName != weapon_bareHand) wep.RangeActivator(true);
                }
                else
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
    /// ��������`�F�b�N
    /// �E�f��ȊO�̕���𑕔������`�F�b�N
    /// �E���킷�ׂĂɎ����̃^�O��K�p
    /// �E����Ɏ����̃X�e�[�^�X���̃A�N�Z�b�T�[��n��
    /// </summary>
    void CheckArmed()
    {
        isArmed = false;
        foreach (WeaponInfo wep in weapons)
        {
            wep.tag = this.gameObject.tag;
            wep.Status = status;

            if (isArmed || wep.WeaponName == weapon_bareHand) continue;

            isArmed = true;
        }
    }
}
