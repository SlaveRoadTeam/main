using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��U���͈�
/// </summary>
[RequireComponent(typeof(Collider))]
public class DamageRange : MonoBehaviour
{
    /// <summary>
    /// �Ή��L�����N�^�[�̃X�e�[�^�X
    /// </summary>
    [SerializeField]
    CharacterStatus status = default;

    /// <summary>
    /// ��U���͈̓R���C�_�[
    /// </summary>
    Collider[] ranges = default;

    /// <summary>
    /// �U�����q�b�g�����Ƃ��ɔ���������p�[�e�B�N��
    /// </summary>
    [SerializeField]
    GameObject[] hitEffects = default;


    /// <summary>
    /// �_���[�W���󂯂�
    /// </summary>
    bool isDamaged = false;

    /// <summary>
    /// ���U��������
    /// </summary>
    bool isHardHit = false;


    /// <summary>
    /// �_���[�W���󂯂�����(-�΁`0�`��)
    /// </summary>
    float damagedDirection = 0.0f;


    public bool IsDamaged { get => isDamaged; set => isDamaged = value; }
    public bool IsHardHit { get => isHardHit; }
    public float DamagedDirection { get => damagedDirection; }
    


    // Start is called before the first frame update
    void Start()
    {
        ranges = GetComponents<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        //�|���ꂽ��A�_���[�W���󂯂�R���C�_�[�𖳌���
        if(status.IsDefeated)
        {
            foreach(Collider range in ranges)
            {
                range.enabled = false;
            }
        }
    }


    /// <summary>
    /// �G�Ύ҂���̍U�����󂯁AHP�����炷
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if((other.CompareTag("Enemy") && transform.CompareTag("Player"))
            || (other.CompareTag("Player") && transform.CompareTag("Enemy")))
        {
            WeaponInfo attacker = other.gameObject.GetComponent<WeaponInfo>();
            if (!attacker) return;

            //�_���[�W��������
            isDamaged = true;

            //���U�����̔���
            isHardHit = attacker.DoStrongAttack;

            //�p�[�e�B�N�������葱��
            if(hitEffects.Length > 0)
            {
                foreach(GameObject effect in hitEffects)
                {
                    if (effect)
                    {
                        //�p�[�e�B�N������
                        GameObject initialized = Instantiate(effect);
                        initialized.transform.position = other.ClosestPoint(other.transform.position);
                        Destroy(initialized, 3.0f);
                    }
                }
            }

            //�ǂ̕�������󂯂��������߂�
            Vector3 vec = Vector3.Normalize(other.transform.position - this.transform.position);
            damagedDirection = Vector3.SignedAngle(this.transform.forward, vec, this.transform.up);

            //�_���[�W�v�Z(��)
            int damage = (int)((attacker.Status.Power + attacker.WeaponPower) * attacker.PowerRatio);

            //HP�����炷
            status.NowHp = Mathf.Max(status.NowHp - damage, 0);
        }
    }
}
