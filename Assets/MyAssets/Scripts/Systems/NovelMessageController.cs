using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �m�x���Q�[���̉�b�p�[�g�̂悤�ɁA���͂����X�ɕ\���A�{�^���Ŏ��ɂ����ނȂǂ𐧌䂷��
/// </summary>
public class NovelMessageController : MonoBehaviour
{
    /// <summary>
    /// ���͂߂���Ɏg���{�^����
    /// </summary>
    [SerializeField]
    string useButton = default;


    /// <summary>
    /// ���b�Z�[�W��\������e�L�X�g�I�u�W�F�N�g
    /// </summary>
    [SerializeField]
    Text messageText = default;

    /// <summary>
    /// �����҂̖��O��\������e�L�X�g�I�u�W�F�N�g
    /// </summary>
    [SerializeField]
    Text nameText = default;


    /// <summary>
    /// ���b�Z�[�W�E�B���h�E���N���b�N�����Ƃ��ɗ�������t���O
    /// </summary>
    bool isClickedMessageWindow = false;

    /// <summary>
    /// ���͂߂���v���t���O
    /// </summary>
    bool isRequestProceedNextMessage = false;

    /// <summary>
    /// �x���\��������t���O
    /// </summary>
    bool isDelaySkip = false;

    /// <summary>
    /// �A�N�V�������s���t���O
    /// </summary>
    [SerializeField]
    bool isRunnableAction = false;

    /// <summary>
    /// �S�A�N�V�������s�ς݃t���O
    /// </summary>
    [SerializeField]
    bool isRunAllActions = false;

    /// <summary>
    /// �R���[�`�����̕����\���x���ɗ��p����N���X�C���X�^���X
    /// </summary>
    WaitForSeconds waitForSeconds = default;


    /// <summary>
    /// ���͊i�[��
    /// </summary>
    [System.Serializable]
    public class MessageContainer
    {
        /// <summary>
        /// ���O
        /// </summary>
        [SerializeField]
        string name = "���O";

        /// <summary>
        /// ���̔���������҂̖��O
        /// </summary>
        [SerializeField]
        string whose = "���̔���������҂̖��O";

        /// <summary>
        /// ���̖͂{��
        /// </summary>
        [SerializeField, TextArea(1,10)]
        string sentence = "����";

        /// <summary>
        /// �\�����̕���
        /// </summary>
        string disclosuredSentence = "";

        /// <summary>
        /// ���͂̕\�����x
        /// </summary>
        [SerializeField]
        float speed = 0.05f;

        /// <summary>
        /// ���͂����ׂĕ\�����I����
        /// </summary>
        bool isDisclosuredAll = false;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public MessageContainer()
        {
            name = "���O";
            whose = "���̔���������҂̖��O";
            sentence = "����";
            disclosuredSentence = "";
            speed = 0.05f;
            isDisclosuredAll = false;
        }

        public string Whose { get => whose; }
        public string DisclosuredSentence { get => disclosuredSentence; }
        public bool IsDisclosuredAll { get => isDisclosuredAll; }
        public float Speed { get => speed; }

        /// <summary>
        /// ���͕\��
        /// </summary>
        public void Show()
        {
            //�����񂪂Ȃ���΁A�S���\�����������Ƃ���
            isDisclosuredAll = (sentence.Length <= 0);

            //���͂����ׂĕ\�����I����܂ŁA
            if (!isDisclosuredAll)
            {
                //�u�\�����̕��́v�Ɂu�\�����̕��́v�̒���+1�Ԗڂ�sentence�̕�����ǉ�����
                disclosuredSentence += sentence[disclosuredSentence.Length];

                //�u�\�����̕��́v�Ɓu���̖͂{���v�̒�������v������u���͂����ׂĕ\�����I�����v��ԂƂ���
                isDisclosuredAll = (disclosuredSentence.Length == sentence.Length);
            }
        }
    }
    [SerializeField]
    MessageContainer[] messageContainer = default;

    public bool IsRunnableAction { set => isRunnableAction = value; }
    public bool IsRunAllActions { get => isRunAllActions; }

    /// <summary>
    /// ���͂��쐬
    /// </summary>
    /// <returns></returns>
    IEnumerator CreateMessage()
    {
        //�A�N�V���������s����t���O�����܂ő҂�
        while (!isRunnableAction)
        {
            //���̃t���[����
            yield return null;
        }

        foreach (MessageContainer mc in messageContainer)
        {
            //�\�������񏉊���
            if(messageText) messageText.text = "";
            if(nameText) nameText.text = mc.Whose;

            //�x���b���ݒ�
            waitForSeconds = new WaitForSeconds(mc.Speed);

            //�����̒x���\��
            while (!mc.IsDisclosuredAll)
            {
                mc.Show();
                messageText.text = mc.DisclosuredSentence;

                //�x����҂����ɕ\��
                if (isDelaySkip) continue;
                yield return waitForSeconds;
            }

            //�{�^�����������܂ő҂�
            do
            {
                //���̃t���[����
                yield return null;
            } while (!isRequestProceedNextMessage);

            //�x���X�L�b�v�t���O��܂�
            isDelaySkip = false;
        }

        //�S�A�N�V�������s�ς݃t���O�𗧂Ă�
        isRunAllActions = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        //�u���͂��쐬�v�R���[�`�����s
        StartCoroutine(CreateMessage());
    }

    // Update is called once per frame
    void Update()
    {
        RequestProceedNextMessage();

        //���͕\���x���t���O�𗧂Ă�
        if (isRunnableAction && !isRunAllActions && !isDelaySkip && isRequestProceedNextMessage)
        {
            isDelaySkip = true;
        }
    }





    /// <summary>
    /// �m�x�����b�Z�[�W�\���𑀍삷��{�^�����͂��W�񂷂郁�\�b�h
    /// </summary>
    void RequestProceedNextMessage()
    {
        isRequestProceedNextMessage = (isClickedMessageWindow || Input.GetButtonDown(useButton));
        isClickedMessageWindow = false;
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
