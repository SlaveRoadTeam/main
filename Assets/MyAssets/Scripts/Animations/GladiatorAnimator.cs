using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GladiatorAnimator : MonoBehaviour
{
    [Header("Animator�ɓn���p�����[�^�[��")]
    /// <summary>
    /// �p�����[�^�[���F���폊���t���O
    /// </summary>
    [SerializeField, Tooltip("�p�����[�^�[���F���폊���t���O")] protected string ParamIsArmed = "isArmed";
    /// <summary>
    /// �p�����[�^�[���FX�����x
    /// </summary>
    [SerializeField, Tooltip("�p�����[�^�[���FX�����x")] protected string ParamMoveDirectionX = "moveDirectionX";
    /// <summary>
    /// �p�����[�^�[���FY�����x
    /// </summary>
    [SerializeField, Tooltip("�p�����[�^�[���FY�����x")] protected string ParamMoveDirectionY = "moveDirectionY";
    /// <summary>
    /// �p�����[�^�[���F���s�t���O
    /// </summary>
    [SerializeField, Tooltip("�p�����[�^�[���F���s�t���O")] protected string ParamIsRunning = "isRunning";
    /// <summary>
    /// �p�����[�^�[��:�U���t���O
    /// </summary>
    [SerializeField, Tooltip("�p�����[�^�[���F�U���t���O")] protected string ParamIsAttack = "isAttack";
    /// <summary>
    /// �p�����[�^�[���F���U���t���O
    /// </summary>
    [SerializeField, Tooltip("�p�����[�^�[���F���U�����s�t���O")] protected string ParamIsStrongAttack = "isStrongAttack";
    /// <summary>
    /// �p�����[�^�[���F���U���t���O
    /// </summary>
    [SerializeField, Tooltip("�p�����[�^�[���F��_���[�W�t���O")] protected string ParamIsDamaged = "isDamaged";
    /// <summary>
    /// �p�����[�^�[���F���U���t���O
    /// </summary>
    [SerializeField, Tooltip("�p�����[�^�[���F���U����e�t���O")] protected string ParamIsHardHit = "isHardHit";
    /// <summary>
    /// �p�����[�^�[���F��_���[�W����(���ʂ�0��)
    /// </summary>
    [SerializeField, Tooltip("�p�����[�^�[���F��_���[�W����(���ʂ�0��)")] protected string ParamDamageDirectionAngle = "damageDirectionAngle";
    /// <summary>
    /// �p�����[�^�[���F�s���s�\�t���O
    /// </summary>
    [SerializeField, Tooltip("�p�����[�^�[���F�s���s�\�t���O")] protected string ParamIsDefeated = "isDefeated";



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
