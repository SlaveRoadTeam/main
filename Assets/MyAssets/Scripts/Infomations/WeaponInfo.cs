using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ������
/// </summary>
[RequireComponent(typeof(Collider))]
public class WeaponInfo : MonoBehaviour
{
    /// <summary>
    /// �Ή��L�����N�^�[�̃X�e�[�^�X
    /// </summary>
    CharacterStatus status = default;


    /// <summary>
    /// �U���͈̓R���C�_�[
    /// </summary>
    Collider[] ranges = default;


    /// <summary>
    /// ���햼
    /// </summary>
    [SerializeField]
    string weaponName = "�f��";

    /// <summary>
    /// ����U����
    /// </summary>
    [SerializeField]
    int weaponPower = 10;


    /// <summary>
    /// ���̕���ōU������Ƃ��̋��U���t���O
    /// </summary>
    bool doStrongAttack = false;
    /// <summary>
    /// ���̕���ōU������Ƃ��̈З͕␳
    /// </summary>
    float powerRatio = 1.0f;



    /* �v���p�e�B */
    public CharacterStatus Status { get => status; set => status = value; }
    public int WeaponPower { get => weaponPower; }
    public string WeaponName { get => weaponName; }
    public bool DoStrongAttack { get => doStrongAttack; set => doStrongAttack = value; }
    public float PowerRatio { get => powerRatio; set => powerRatio = value; }




    // Start is called before the first frame update
    void Start()
    {
        ranges = GetComponents<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// �U���͈̓R���C�_�[���Aflag��true�ŋN���Afalse�ŏI��
    /// </summary>
    public void RangeActivator(bool flag)
    {
        foreach(Collider range in ranges)
        {
            range.enabled = flag;
        }
    }
}
