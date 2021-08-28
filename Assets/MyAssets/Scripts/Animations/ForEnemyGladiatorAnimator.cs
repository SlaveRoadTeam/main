using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �G�̃��f���̃A�j���[�^�[�Ƀp�����[�^�[��n���R���|�[�l���g
/// </summary>
[RequireComponent(typeof(Animator))]
public class ForEnemyGladiatorAnimator : GladiatorAnimator
{
    /// <summary>
    /// �Ώۂ�AI�s���R���|�[�l���g
    /// </summary>
    Enemy1AIController aIController = default;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        aIController = GetComponentInParent<Enemy1AIController>();
        status = GetComponentInParent<CharacterStatus>();
        damageRange = GetComponentInParent<DamageRange>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!aIController.IsAcceptOtherActions) return;

        

        //�ړ����[�V�����́A�s����Ԃɂ���ė^����l��ς���
        Vector2 md = new Vector2(aIController.Navmesh.velocity.x, aIController.Navmesh.velocity.z);
        if (aIController.AiState == Enemy1AIController.AIState.Confronting)
        {
            animator.SetFloat(ParamMoveDirectionX, md.x);
            animator.SetFloat(ParamMoveDirectionY, md.y);
        }
        else
        {
            animator.SetFloat(ParamMoveDirectionX, 0.0f);
            animator.SetFloat(ParamMoveDirectionY, md.magnitude);
        }
        animator.SetBool(ParamIsArmed, aIController.IsArmed);
        animator.SetBool(ParamIsRunning, (status.IsRunning && md.sqrMagnitude > 0.0f));

        //��U���������
        if (aIController.DoCommonAttack)
        {
            aIController.IsAcceptOtherActions = false;
            animator.SetTrigger(ParamIsAttack);
            animator.SetBool(ParamIsStrongAttack, false);
        }
        //���U���������
        else if (aIController.DoStrongAttack)
        {
            aIController.IsAcceptOtherActions = false;
            animator.SetTrigger(ParamIsAttack);
            animator.SetBool(ParamIsStrongAttack, true);
        }



        //�U����������
        if (damageRange.IsDamaged)
        {
            damageRange.IsDamaged = false;
            aIController.IsAcceptOtherActions = false;
            animator.SetTrigger(ParamIsDamaged);
            animator.SetBool(ParamIsHardHit, damageRange.IsHardHit);
            animator.SetFloat(ParamDamageDirectionAngle, damageRange.DamagedDirection);
            if (status.IsDefeated) animator.SetTrigger(ParamIsDefeated);
        }
    }


    public new void AttackStart()
    {
        aIController.IsAttacking = true;
    }

    public new void AttackEnd()
    {
        aIController.IsAttacking = false;
    }

    public new void AcceptOtherActions()
    {
        aIController.IsAcceptOtherActions = true;
    }
}
