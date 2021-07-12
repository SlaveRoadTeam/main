using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GladiatorAnimator : MonoBehaviour
{
    [Header("Animator�ɓn���p�����[�^�[��")]
    /// <summary>
    /// �p�����[�^�[���F���폊���t���O
    /// </summary>
    [SerializeField, Tooltip("�p�����[�^�[���F���폊���t���O")] protected string Param_isArmed = "���������Ă��邩";
    /// <summary>
    /// �p�����[�^�[���FX�����x
    /// </summary>
    [SerializeField, Tooltip("�p�����[�^�[���FX�����x")] protected string Param_moveDirectionX = "X���̑��x";
    /// <summary>
    /// �p�����[�^�[���FY�����x
    /// </summary>
    [SerializeField, Tooltip("�p�����[�^�[���FY�����x")] protected string Param_moveDirectionY = "Y���̑��x";
    /// <summary>
    /// �p�����[�^�[���F���s�t���O
    /// </summary>
    [SerializeField, Tooltip("�p�����[�^�[���F���s�t���O")] protected string Param_isRunning = "�����Ă��邩";
    /// <summary>
    /// �p�����[�^�[��:�U���t���O
    /// </summary>
    [SerializeField, Tooltip("�p�����[�^�[���F�U���t���O")] protected string Param_isAttack = "�U���w������������";
    /// <summary>
    /// �p�����[�^�[���F���U���t���O
    /// </summary>
    [SerializeField, Tooltip("�p�����[�^�[���F���U�����s�t���O")] protected string Param_isStrongAttack = "�������U�������U�����ǂ���";
    /// <summary>
    /// �p�����[�^�[���F���U���t���O
    /// </summary>
    [SerializeField, Tooltip("�p�����[�^�[���F��_���[�W�t���O")] protected string Param_isDamaged = "�_���[�W���󂯂���";
    /// <summary>
    /// �p�����[�^�[���F���U���t���O
    /// </summary>
    [SerializeField, Tooltip("�p�����[�^�[���F���U����e�t���O")] protected string Param_isHardHit = "�󂯂��U�������U�����ǂ���";
    /// <summary>
    /// �p�����[�^�[���F��_���[�W����(���ʂ�0��)
    /// </summary>
    [SerializeField, Tooltip("�p�����[�^�[���F��_���[�W����(���ʂ�0��)")] protected string Param_damageDirectionAngle = "���ʂ�0���Ƃ������̔�_���[�W�����p�x";
    /// <summary>
    /// �p�����[�^�[���F�s���s�\�t���O
    /// </summary>
    [SerializeField, Tooltip("�p�����[�^�[���F�s���s�\�t���O")] protected string Param_isDefeated = "�|���ꂽ��";



    /// <summary>
    /// �Ώۂ̃A�j���[�^�[
    /// </summary>
    protected Animator animator = default;

    /// <summary>
    /// �Ώۂ̃X�e�[�^�X
    /// </summary>
    protected CharacterStatus status = default;

    /// <summary>
    /// �Ώۂ̃_���[�W�����R���|�[�l���g
    /// </summary>
    protected DamageRange damageRange = default;


    public void AttackStart()
    {

    }

    public void AttackEnd()
    {

    }

    public void AcceptOtherActions()
    {

    }

    public void Hit()
    {

    }

    public void FootR()
    {

    }

    public void FootL()
    {

    }
}
