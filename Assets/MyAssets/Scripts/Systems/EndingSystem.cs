using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �G���f�B���O��ʂ𐧌䂷��
/// </summary>
public class EndingSystem : MonoBehaviour
{
    [SerializeField]
    string nextSceneName = "Title";

    [SerializeField]
    string useButton = "Submit";

    bool isClickedMessageWindow = false;

    [SerializeField]
    GameObject praiseMessage = default;

    [SerializeField]
    NovelMessageController novelMessageController = default;

    [SerializeField]
    SceneChanger sceneChanger = default;


    // Start is called before the first frame update
    void Start()
    {
        praiseMessage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (novelMessageController.IsRunAllActions)
        {
            praiseMessage.SetActive(true);

            if (Input.GetButtonDown(useButton) || isClickedMessageWindow) sceneChanger.GoScene(nextSceneName);
        }
        else
        {
            isClickedMessageWindow = false;
        }
    }

    /// <summary>
    /// ��ʏ�̃{�^�����͂ɂ�葀�삷�邽�߂̃��\�b�h
    /// Button��OnClick�ɂČĂяo��
    /// </summary>
    public void ClickedMessageWindow()
    {
        isClickedMessageWindow = true;
    }
}
