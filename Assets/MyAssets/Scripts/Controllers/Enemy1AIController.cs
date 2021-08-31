using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// �G�̍s��
/// </summary>
public class Enemy1AIController : MonoBehaviour
{
    /// <summary>
    /// �G�̍s�����j
    /// </summary>
    public enum AIType
    {
        /// <summary>
        /// �����߂Â��Đ��ʂ���U��
        /// </summary>
        Breakthrough,
        /// <summary>
        /// ����̍s�����_���čU��
        /// </summary>
        AfterTheMotion,
        /// <summary>
        /// ����̌��Ɍ������悤�ɓ����čU��
        /// </summary>
        AroundToTheBack
    }
    [SerializeField]
    AIType aiType = AIType.Breakthrough;

    /// <summary>
    /// �G�̍s����
    /// </summary>
    public enum AIState : byte
    {
        /// <summary>
        /// �ҋ@
        /// </summary>
        Idle,
        /// <summary>
        /// �K���Ɉړ�
        /// </summary>
        Wander,
        /// <summary>
        /// �ǂ�������
        /// </summary>
        Seek,
        /// <summary>
        /// �Λ����Ă���
        /// </summary>
        Confronting,
        /// <summary>
        /// �U��
        /// </summary>
        Attack,
        /// <summary>
        /// �������Ƃ�
        /// </summary>
        Flee,
        /// <summary>
        /// �|���ꂽ
        /// </summary>
        Defeated
    }
    [SerializeField]
    AIState aiState = AIState.Idle;


    /// <summary>
    /// ����(�f��)���
    /// </summary>
    WeaponInfo[] weapons = default;

    /// <summary>
    /// �f��̕��햼
    /// </summary>
    [SerializeField, Tooltip("�f��̏ꍇ�̕��햼")] string weaponBareHand = "�f��";


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
    /// �U���͈͂��L���ł���(ForEnemyGladiatorAnimator����ύX)
    /// </summary>
    bool isAttacking = false;
    /// <summary>
    /// ���̃A�N�V�����̎�t�������Ă��邩(ForEnemyGladiatorAnimator����ύX)
    /// </summary>
    bool isAcceptOtherActions = true;


    /// <summary>
    /// Idle��Ԃ̑ҋ@����
    /// </summary>
    float idleTimelimit = 0.0f;
    /// <summary>
    /// timelimit_Idle�̃^�C���A�b�v�J�E���^�[
    /// </summary>
    float idleTimer = 0.0f;


    /// <summary>
    /// �i�r���b�V��
    /// </summary>
    NavMeshAgent navmesh = default;
    /// <summary>
    /// �i�r���b�V�����w�肵���n�`�I�u�W�F�N�g�̃��C��
    /// </summary>
    [SerializeField]
    LayerMask navmeshGroundLayers = default;
    /// <summary>
    /// �D��I�x��(0�ł܂������U�����Ȃ��A1�ŊԔ����ꂸ�U����������)
    /// </summary>
    [SerializeField, Range(0.0f, 1.0f)]
    float warlikeRatio = 0.2f;
    /// <summary>
    /// �ړ���ړI�n�̊�l
    /// </summary>
    [SerializeField]
    GameObject destinationBase = default;
    /// <summary>
    /// �ǐՁE�U���Ώۂ̃I�u�W�F�N�g
    /// </summary>
    [SerializeField]
    GameObject target = default;
    /// <summary>
    /// �ǐՁE�U���Ώۂ̃X�e�[�^�X
    /// </summary>
    CharacterStatus targetStatus = default;
    /// <summary>
    /// �Λ�����ۂɂƂ鋗��
    /// </summary>
    float confrontDistance = 1.0f;
    /// <summary>
    /// �Λ���ēx�ǐՂ��J�n����A�����ꂽ����
    /// </summary>
    float seekAgainDistance = 4.0f;

    /// <summary>
    /// �ړ����̖ړI�n
    /// </summary>
    List<Vector3> destinations = new List<Vector3>();





    /* �v���p�e�B */
    public bool DoCommonAttack { get => doCommonAttack; }
    public bool DoStrongAttack { get => doStrongAttack; }
    public bool IsArmed { get => isArmed; }
    public bool IsAttacking { set => isAttacking = value; }
    public bool IsAcceptOtherActions { get => isAcceptOtherActions; set => isAcceptOtherActions = value; }
    public NavMeshAgent Navmesh { get => navmesh; }
    public AIState AiState { get => aiState; }
    public AIType AiType { get => aiType; }





    // Start is called before the first frame update
    void Start()
    {
        if (!destinationBase)
        {
            destinationBase = new GameObject();
            destinationBase.transform.position = Vector3.zero;
        }

        navmesh = GetComponent<NavMeshAgent>();
        weapons = GetComponentsInChildren<WeaponInfo>();
        status = GetComponentInChildren<CharacterStatus>();
        CheckArmed();
    }

    // Update is called once per frame
    void Update()
    {
        //�|���ꂽ��ԂȂ瑦������
        if (aiState == AIState.Defeated) return;

        //�U���t���O������
        doCommonAttack = false;
        doStrongAttack = false;

        //�U���A�j���[�V�������A�_���[�W����̂��铮���ɓ����Ă���
        if (isAttacking)
        {
            //���했�ɓ����蔻��R���C�_�[���N��
            foreach (WeaponInfo wep in weapons)
            {
                if (isArmed)
                {
                    if (wep.WeaponName != weaponBareHand) wep.RangeActivator(true);
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

        //������󂯕t���Ȃ���ԂȂ瑦������
        if (!isAcceptOtherActions) return;

        //�������A�̗͂��s�����ꂽ
        if (status.IsDefeated)
        {
            //�ړI�n�폜
            destinations = new List<Vector3>();

            //�����I�Ɍo�H���s�𒆒f
            navmesh.isStopped = true;

            //Defeated��Ԃ�
            aiState = AIState.Defeated;
        }

        //�ǐՁE�U���Ώۂ��A�̗͂��s�����ꂽ
        if (targetStatus && targetStatus.IsDefeated)
        {
            //�ǐՁE�U���Ώۂ�����
            target = null;

            //�ǐՁE�U���Ώۂ̃X�e�[�^�X�������ɉ���
            targetStatus = null;

            //Idle��Ԃ�
            aiState = AIState.Idle;
        }

        

        //�s����Ԃɉ����ĕ���
        switch (aiState)
        {
            case AIState.Idle:
                {
                    StateIdle();
                    break;
                }
            case AIState.Wander:
                {
                    StateWander();
                    break;
                }
            case AIState.Seek:
                {
                    StateSeek();
                    break;
                }
            case AIState.Confronting:
                {
                    StateConfronting();
                    break;
                }
            case AIState.Attack:
                {
                    StateAttack();
                    break;
                }
            case AIState.Flee:
                {
                    StateFlee();
                    break;
                }
            case AIState.Defeated:
                {
                    break;
                }
            default: break;
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

            if (isArmed || wep.WeaponName == weaponBareHand) continue;

            isArmed = true;
        }
    }


    /// <summary>
    /// �ҋ@���
    /// </summary>
    void StateIdle()
    {
        //�^�C�����~�b�g������`(�ҋ@��ԂɂȂ����΂���)�Ȃ�A
        //�ҋ@���Ԃ�0.1�b�`5.0�b�Ō���
        if (idleTimelimit <= 0.0f) idleTimelimit = Random.Range(0.1f, 5.0f);
            
        //�J�E���g�A�b�v
        idleTimer += Time.deltaTime;

        //�^�C�����~�b�g�ɓ��B����
        if (idleTimelimit <= idleTimer)
        {
            //�^�C�����~�b�g������
            idleTimelimit = 0.0f;
            //�J�E���^�[������
            idleTimer = 0.0f;

            //Wander��Ԃ�
            aiState = AIState.Wander;
        }

        //�ǐՁE�U���Ώۂ�������
        if (target)
        {
            //�ǐՁE�U���Ώۂ̃X�e�[�^�X���擾
            targetStatus = target.GetComponentInChildren<CharacterStatus>();

            //Seek��Ԃ�
            aiState = AIState.Seek;
        }
    }
    /// <summary>
    /// �����_���ړ����
    /// </summary>
    void StateWander()
    {
        //�ړI�n���W������̏ꍇ
        if(destinations.Count <= 0)
        {
            //�ړI�n�ƂȂ鐅���ʒu�̏��10m���x�ɂ�������W�_���쐬
            Vector3 destinationBasePos = destinationBase.transform.position;
            Vector3 destination2D = new Vector3(Random.Range(-10.0f, 10.0f) + destinationBasePos.x,
                                                destinationBasePos.y + 10.0f,
                                                Random.Range(-10.0f, 10.0f) + destinationBasePos.z);

            //���10m���x�ɂ�������W�_����^���̒n�`�Ɍ���Ray�𗎂Ƃ�
            RaycastHit hitInfo;
            bool isfound = Physics.Raycast(destination2D, Vector3.down, out hitInfo, 1000, navmeshGroundLayers);

            //�n�`�ɓ���������
            if (isfound)
            {
                //���̈ʒu���L�^
                destinations.Add(hitInfo.point);

                //�ړ���Ɍ���
                navmesh.SetDestination(destinations[0]);

                //�o�H���s���J�n
                navmesh.isStopped = false;
            }
        }

        //�ړI�n�ɋ߂Â���
        if (Vector3.SqrMagnitude(transform.position - destinations[0]) <= Mathf.Pow(1.0f, 2.0f))
        {
            //�ړI�n�폜
            destinations = new List<Vector3>();

            //Idle��Ԃ�
            aiState = AIState.Idle;
        }

        //�ǐՁE�U���Ώۂ�������
        if (target)
        {
            //Seek��Ԃ�
            aiState = AIState.Seek;
        }
    }
    /// <summary>
    /// �ǂ��������
    /// </summary>
    void StateSeek()
    {
        //�������W�ɂ�����ړI�n
        Vector3 destination2D = target.transform.position;

        //���s���
        status.IsRunning = true;
        navmesh.speed = 5.0f;

        //��荞�ݍ��Ȃ�
        if (aiType == AIType.AroundToTheBack)
        {
            //�^�[�Q�b�g���猩�ĉE���������ǂ���ɂ��邩
            if(Vector3.SqrMagnitude(transform.position - (destination2D + target.transform.right)) < Vector3.SqrMagnitude(transform.position - (destination2D - target.transform.right)))
            {
                //����̉E�����ڎw��
                destination2D += target.transform.right * 1.5f;
            }
            else
            {
                //����̍������ڎw��
                destination2D -= target.transform.right * 1.5f;
            }
            destination2D -= target.transform.forward;
        }

        //�ǐՁE�U���ΏۂƂȂ�G�̐����ʒu�̏��10m���x�ɂ�������W�_���쐬
        destination2D += Vector3.up * 10.0f;

        //���10m���x�ɂ�������W�_����^���̒n�`�Ɍ���Ray�𗎂Ƃ�
        RaycastHit hitInfo;
        bool isfound = Physics.Raycast(destination2D, Vector3.down, out hitInfo, 1000, navmeshGroundLayers);

        //Ray���n�`�ɓ���������
        if (isfound)
        {
            //���̈ʒu���L�^
            destinations.Add(hitInfo.point);

            //�ړ���Ɍ���
            navmesh.SetDestination(destinations[0]);

            //�o�H���s���J�n
            navmesh.isStopped = false;
        }

        //�ǐՑΏۂɁA�Ƃ�ׂ��ԍ����܂Őڋ߂���
        if (Vector3.SqrMagnitude(transform.position - destinations[0]) <= Mathf.Pow(confrontDistance, 2.0f))
        {
            //�ړI�n�폜
            destinations = new List<Vector3>();

            //�����I�Ɍo�H���s�𒆒f
            navmesh.isStopped = true;

            //���s���
            status.IsRunning = false;
            navmesh.speed = 3.5f;

            //Confronting��Ԃ�
            aiState = AIState.Confronting;
        }
    }
    /// <summary>
    /// �Λ����Ă�����
    /// </summary>
    void StateConfronting()
    {
        //���target�̕�������������
        transform.LookAt(target.transform);

        //�ړI�n���W������̏ꍇ
        if (destinations.Count <= 0)
        {
            /* �ȉ��Atarget�̎��͂���񂷂�悤�ȖړI�n��ݒ� */
            Vector3 destination2D = transform.position 
                                    + transform.right * Random.Range(-2.0f, 2.0f) 
                                    + Vector3.up * 10.0f;

            //���10m���x�ɂ�������W�_����^���̒n�`�Ɍ���Ray�𗎂Ƃ�
            RaycastHit hitInfo;
            bool isfound = Physics.Raycast(destination2D, Vector3.down, out hitInfo, 1000, navmeshGroundLayers);

            //Ray���n�`�ɓ���������
            if (isfound)
            {
                //���̈ʒu���L�^
                destinations.Add(hitInfo.point);

                //�ړ���Ɍ���
                navmesh.SetDestination(destinations[0]);

                //�o�H���s���J�n
                navmesh.isStopped = false;
            }
        }

        //�ړI�n�ɋ߂Â���
        if (Vector3.SqrMagnitude(transform.position - destinations[0]) <= Mathf.Pow(0.1f, 2.0f))
        {
            //�ړI�n�폜
            destinations = new List<Vector3>();

            //�D��I�x���Ɋ�Â��āA�U�����ēx�ړ���������
            if(Random.value <= warlikeRatio)
            {
                //�o�H���s���I��
                navmesh.isStopped = true;

                //Attack��Ԃ�
                aiState = AIState.Attack;
            }
        }

        //�Λ��ΏۂɁA�ԍ����𗣂��ꂽ
        if (Vector3.SqrMagnitude(transform.position - target.transform.position) >= Mathf.Pow(seekAgainDistance, 2.0f))
        {
            //Seek��Ԃ�
            aiState = AIState.Seek;
        }
    }
    /// <summary>
    /// �U�����
    /// </summary>
    void StateAttack()
    {
        //���̑�����͂������Ă���
        if (isAcceptOtherActions)
        {
            //�Λ��ΏۂɁA�ԍ����𗣂��ꂽ
            if (Vector3.SqrMagnitude(transform.position - target.transform.position) >= Mathf.Pow(seekAgainDistance, 2.0f))
            {
                //Seek��Ԃ�
                aiState = AIState.Seek;
            }
            //�D��I�x���ɉ����āA�U�����Λ��ɑJ��
            else if (Random.value > warlikeRatio)
            {
                //�Λ��ɑJ�ڂ̏ꍇ�AConfronting��Ԃ�
                aiState = AIState.Confronting;
            }
            //�ԍ����͂Ƃ��Ă��Ȃ��A�U���ɑJ�ڂ����ꍇ�͍U��
            else
            {
                doCommonAttack = true;

                //����ɁA���U���ł��邩�̏��ƈЗ͕␳����n��
                foreach (WeaponInfo wep in weapons)
                {
                    wep.DoStrongAttack = doStrongAttack;

                    wep.PowerRatio = 1.0f;
                }
            }
        }
    }
    /// <summary>
    /// �������Ƃ���
    /// </summary>
    void StateFlee()
    {
        
    }
}
