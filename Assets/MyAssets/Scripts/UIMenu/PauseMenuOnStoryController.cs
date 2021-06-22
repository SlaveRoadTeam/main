using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenuOnStoryController : MonoBehaviour
{
    /// <summary>
    /// ���j���[�w�i
    /// </summary>
    [SerializeField] GameObject menuBackground = default;

    //���j���[�{�^��
    [SerializeField] Button menuButton = default;


    /// <summary>
    /// ���C�����j���[
    /// </summary>
    [SerializeField] GameObject mainMenu = default;
    [SerializeField] Button[] mainMenuButtons = default;
    [SerializeField] GameObject mainMenuFirstFocus = default;

    /// <summary>
    /// �m�F���j���[
    /// </summary>
    [SerializeField] GameObject confirmMenu = default;
    [SerializeField] Button[] confirmMenuButtons = default;
    [SerializeField] GameObject confirmMenuFirstFocus = default;

    /// <summary>
    /// ����m�F�\���j���[
    /// </summary>
    [SerializeField] GameObject instructionMenu = default;
    [SerializeField] Button buckButton = default;


    /// <summary>
    /// StartCoroutine�ɂ��A���̃t���[���Ń{�^���Ƀt�H�[�J�X�𓖂Ă�
    /// </summary>
    /// <param name="buttonObj"></param>
    /// <returns></returns>
    IEnumerator FocusButtonNextFrame(GameObject buttonObj)
    {
        yield return null;
        yield return null;

        EventSystem.current.SetSelectedGameObject(buttonObj);
    }


    //���j���[���J��
    public void OpenMenu()
    {
        menuBackground.SetActive(true);

        instructionMenu.SetActive(false);
        mainMenu.SetActive(true);
        confirmMenu.SetActive(false);

        menuButton.interactable = false;

        StartCoroutine(FocusButtonNextFrame(mainMenuFirstFocus));
    }

    //�m�F���j���[���J��
    public void OpenConfirmMenu()
    {
        menuBackground.SetActive(true);

        mainMenu.SetActive(false);
        confirmMenu.SetActive(true);

        menuButton.interactable = false;

        StartCoroutine(FocusButtonNextFrame(confirmMenuFirstFocus));
    }


    //���j���[�����
    public void CloseMenu()
    {
        instructionMenu.SetActive(false);
        mainMenu.SetActive(true);
        confirmMenu.SetActive(false);

        menuButton.interactable = true;

        menuBackground.SetActive(false);
    }

    /// <summary>
    /// ����m�F���j���[���J��
    /// </summary>
    public void OpenInstructionMenu()
    {
        instructionMenu.SetActive(true);
        mainMenu.SetActive(false);
        confirmMenu.SetActive(false);

        StartCoroutine(FocusButtonNextFrame(buckButton.gameObject));
    }



    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(mainMenuFirstFocus);
    }

    // Update is called once per frame
    void Update()
    {
        //Cancel(Esc)�{�^���Ń��j���[��ʂ̕\���E��\��
        if (Input.GetButtonDown("Cancel"))
        {
            if (menuBackground.activeSelf) CloseMenu();
            else OpenMenu();
        }
    }
}
