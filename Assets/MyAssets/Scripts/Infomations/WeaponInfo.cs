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
    string name = "����̖��O";

    /// <summary>
    /// ����U����
    /// </summary>
    [SerializeField]
    int weaponPower = 10;



    /* �v���p�e�B */
    public CharacterStatus Status { get => status; set => status = value; }
    public int WeaponPower { get => weaponPower; }
    public string Name { get => name; }




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
