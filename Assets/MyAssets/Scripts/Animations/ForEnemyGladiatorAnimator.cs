using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �G�̃��f���̃A�j���[�^�[�Ƀp�����[�^�[��n���R���|�[�l���g
/// </summary>
[RequireComponent(typeof(Animator))]
public class ForEnemyGladiatorAnimator : GladiatorAnimator
{




    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        status = GetComponentInParent<CharacterStatus>();
        damageRange = GetComponentInParent<DamageRange>();
    }

    // Update is called once per frame
    void Update()
    {
        //�U����������
        if (damageRange.IsDamaged)
        {
            damageRange.IsDamaged = false;
            animator.SetTrigger(Param_isDamaged);
            animator.SetBool(Param_isHardHit, damageRange.IsHardHit);
            animator.SetFloat(Param_damageDirectionAngle, damageRange.DamagedDirection);
            if (status.IsDefeated) animator.SetTrigger(Param_isDefeated);
        }
    }
}
