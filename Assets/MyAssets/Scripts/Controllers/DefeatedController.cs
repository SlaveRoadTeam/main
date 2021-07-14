using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���̃R���|�[�l���g�����Ă���I�u�W�F�N�g�̃��C���ԍ����L�^���A
/// ���ꂽ�ۂɃ��C������ύX����
/// </summary>
public class DefeatedController : MonoBehaviour
{
    /// <summary>
    /// �����̃��C���ԍ�
    /// </summary>
    int originallyLayer = -1;

    /// <summary>
    /// �L�����N�^�[���|���ꂽ�Ƃ��ɏ��������郌�C����
    /// </summary>
    [SerializeField,Tooltip("�|���ꂽ�L�����N�^�[���C��")] string Layer_Defeated = "Defeated";
    int defeatedLayer = -1;

    /// <summary>
    /// �Ώۂ̃L�����N�^�[�X�e�[�^�X
    /// </summary>
    CharacterStatus status = default;


    // Start is called before the first frame update
    void Start()
    {
        status = GetComponentInChildren<CharacterStatus>();
        if (!status) status = GetComponentInParent<CharacterStatus>();

        originallyLayer = this.gameObject.layer;
        defeatedLayer = LayerMask.NameToLayer(Layer_Defeated);
    }

    // Update is called once per frame
    void Update()
    {
        //�|����Ă�����
        if(status.IsDefeated)
        {
            //�|���ꂽ�p�̃��C���ɐ؂�ւ���
            this.gameObject.layer = defeatedLayer;
        }
        else if(this.gameObject.layer != originallyLayer)
        {
            this.gameObject.layer = originallyLayer;
        }
    }
}
