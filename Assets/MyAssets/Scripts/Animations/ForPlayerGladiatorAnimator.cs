using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̃��f���̃A�j���[�^�[�Ƀp�����[�^�[��n���R���|�[�l���g
/// </summary>
[RequireComponent(typeof(Animator))]
public class ForPlayerGladiatorAnimator : MonoBehaviour
{
    [Header("Animator�ɓn���p�����[�^�[��")]
    /// <summary>
    /// �p�����[�^�[���F���폊���t���O
    /// </summary>
    [SerializeField, Tooltip("�p�����[�^�[���F���폊���t���O")] string Param_isArmed = "���������Ă��邩";
    /// <summary>
    /// �p�����[�^�[���FX�����x
    /// </summary>
    [SerializeField, Tooltip("�p�����[�^�[���FX�����x")] string Param_moveDirectionX = "X���̑��x";
    /// <summary>
    /// �p�����[�^�[���FY�����x
    /// </summary>
    [SerializeField, Tooltip("�p�����[�^�[���FY�����x")] string Param_moveDirectionY = "Y���̑��x";
    /// <summary>
    /// �p�����[�^�[���F���s�t���O
    /// </summary>
    [SerializeField, Tooltip("�p�����[�^�[���F���s�t���O")] string Param_isRunning = "�����Ă��邩";
    /// <summary>
    /// �p�����[�^�[��:�U���t���O
    /// </summary>
    [SerializeField, Tooltip("�p�����[�^�[���F�U���t���O")] string Param_isAttack = "�U���w������������";
    /// <summary>
    /// �p�����[�^�[���F���U���t���O
    /// </summary>
    [SerializeField, Tooltip("�p�����[�^�[���F���U���t���O")] string Param_isStrongAttack = "���U�����ǂ���";



    /// <summary>
    /// �Ώۂ̃A�j���[�^�[
    /// </summary>
    Animator animator = default;

    /// <summary>
    /// �Ώۂ̈ړ������R���|�[�l���g
    /// </summary>
    CubeController moveCtrl = default;

    /// <summary>
    /// �Ώۂ̍U�������R���|�[�l���g
    /// </summary>
    PlayerAttackController attackCtrl = default;




    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        moveCtrl = GetComponentInParent<CubeController>();
        attackCtrl = GetComponentInParent<PlayerAttackController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 md = new Vector2(moveCtrl.MoveDirection.x, moveCtrl.MoveDirection.z);
        animator.SetBool(Param_isArmed, attackCtrl.IsArmed);
        animator.SetFloat(Param_moveDirectionX, md.x);
        animator.SetFloat(Param_moveDirectionY, md.y);
        animator.SetBool(Param_isRunning, (moveCtrl.IsRunning && md.sqrMagnitude > 0.0f));

        if (attackCtrl.DoCommonAttack)
        {
            attackCtrl.IsAcceptOtherActions = false;
            animator.SetTrigger(Param_isAttack);
            animator.SetBool(Param_isStrongAttack, false);
        }
        else if (attackCtrl.DoStrongAttack)
        {
            attackCtrl.IsAcceptOtherActions = false;
            animator.SetTrigger(Param_isAttack);
            animator.SetBool(Param_isStrongAttack, true);
        }
        
    }

    public void AttackStart()
    {
        attackCtrl.IsAttacking = true;
    }

    public void AttackEnd()
    {
        attackCtrl.IsAttacking = false;
    }

    public void AcceptOtherActions()
    {
        attackCtrl.IsAcceptOtherActions = true;
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
