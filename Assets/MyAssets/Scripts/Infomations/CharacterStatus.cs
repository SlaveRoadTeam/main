using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    /// <summary>
    /// �ő�̗̑�
    /// </summary>
    [SerializeField]
    int maxHp = 100;
    /// <summary>
    /// ���݂̗̑�
    /// </summary>
    [SerializeField]
    int nowHp = 100;
    /// <summary>
    /// �U����
    /// </summary>
    [SerializeField]
    int power = 30;
    /// <summary>
    /// �X�^�~�i
    /// </summary>
    [SerializeField]
    int stamina = 20;


    /// <summary>
    /// �|���ꂽ��
    /// </summary>
    bool isDefeated = false;



    /* �v���p�e�B */
    public int MaxHp { get => maxHp; set => maxHp = value; }
    public int NowHp { get => nowHp; set => nowHp = value; }
    public int Power { get => power; set => power = value; }
    public int Stamina { get => stamina; set => stamina = value; }
    public bool IsDefeated { get => isDefeated; set => isDefeated = value; }



    // Start is called before the first frame update
    void Start()
    {
        nowHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        isDefeated = nowHp <= 0;
    }


}