using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̃��f���̃A�j���[�^�[�Ƀp�����[�^�[��n���R���|�[�l���g
/// </summary>
[RequireComponent(typeof(Animator))]
public class ForPlayerGladiatorAnimator : GladiatorAnimator
{
    

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
        status = GetComponentInParent<CharacterStatus>();
        moveCtrl = GetComponentInParent<CubeController>();
        attackCtrl = GetComponentInParent<PlayerAttackController>();
        damageRange = GetComponentInParent<DamageRange>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 md = new Vector2(moveCtrl.MoveDirection.x, moveCtrl.MoveDirection.z);
        animator.SetBool(ParamIsArmed, attackCtrl.IsArmed);
        animator.SetFloat(ParamMoveDirectionX, md.x);
        animator.SetFloat(ParamMoveDirectionY, md.y);
        animator.SetBool(ParamIsRunning, (moveCtrl.IsRunning && md.sqrMagnitude > 0.0f));

        //��U���������
        if (attackCtrl.DoCommonAttack)
        {
            attackCtrl.IsAcceptOtherActions = false;
            animator.SetTrigger(ParamIsAttack);
            animator.SetBool(ParamIsStrongAttack, false);
        }
        //���U���������
        else if (attackCtrl.DoStrongAttack)
        {
            attackCtrl.IsAcceptOtherActions = false;
            animator.SetTrigger(ParamIsAttack);
            animator.SetBool(ParamIsStrongAttack, true);
        }


        //�U����������
        if (damageRange.IsDamaged)
        {
            damageRange.IsDamaged = false;
            attackCtrl.IsAcceptOtherActions = false;
            animator.SetTrigger(ParamIsDamaged);
            animator.SetBool(ParamIsHardHit, damageRange.IsHardHit);
            animator.SetFloat(ParamDamageDirectionAngle, damageRange.DamagedDirection);
            if(status.IsDefeated) animator.SetTrigger(ParamIsDefeated);
        }

    }

    public new void AttackStart()
    {
        attackCtrl.IsAttacking = true;
    }

    public new void AttackEnd()
    {
        attackCtrl.IsAttacking = false;
    }

    public new void AcceptOtherActions()
    {
        attackCtrl.IsAcceptOtherActions = true;
    }
}
