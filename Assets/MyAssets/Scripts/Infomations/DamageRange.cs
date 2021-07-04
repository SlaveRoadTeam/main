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
    private CharacterStatus status = default;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if((other.tag == "Enemy" && transform.tag == "Player")
            || (other.tag == "Player" && transform.tag == "Enemy"))
        {
            WeaponInfo attacker = other.gameObject.GetComponent<WeaponInfo>();
            if (!attacker) return;


            //�_���[�W�v�Z(��)
            int damage = attacker.Status.Power + attacker.WeaponPower;

            //HP�����炷
            status.NowHp = Mathf.Max(status.NowHp - damage, 0);
        }

        
    }
}
