using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̃��f���̃A�j���[�^�[�Ƀp�����[�^�[��n���R���|�[�l���g
/// </summary>
[RequireComponent(typeof(Animator))]
public class ForPlayerGladiatorAnimator : MonoBehaviour
{
    /// <summary>
    /// �p�����[�^�[���F���폊���t���O
    /// </summary>
    [SerializeField] string ParamName_isArmed = "���������Ă��邩";
    /// <summary>
    /// �p�����[�^�[���FX�����x
    /// </summary>
    [SerializeField] string ParamName_moveDirectionX = "X���̑��x";
    /// <summary>
    /// �p�����[�^�[���FY�����x
    /// </summary>
    [SerializeField] string ParamName_moveDirectionY = "Y���̑��x";
    /// <summary>
    /// �p�����[�^�[���F���s�t���O
    /// </summary>
    [SerializeField] string ParamName_isRunning = "�����Ă��邩";


    /// <summary>
    /// �Ώۂ̃A�j���[�^�[
    /// </summary>
    Animator animator = default;

    /// <summary>
    /// �Ώۂ̈ړ������R���|�[�l���g
    /// </summary>
    [SerializeField]
    CubeController ctrl = default;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 md = new Vector2(ctrl.MoveDirection.x, ctrl.MoveDirection.z);
        animator.SetBool(ParamName_isArmed, true);
        animator.SetFloat(ParamName_moveDirectionX, md.x);
        animator.SetFloat(ParamName_moveDirectionY, md.y);
        animator.SetBool(ParamName_isRunning, (ctrl.IsRunning && md.sqrMagnitude > 0.0f));
    }

    public void FootR()
    {

    }

    public void FootL()
    {

    }
}
