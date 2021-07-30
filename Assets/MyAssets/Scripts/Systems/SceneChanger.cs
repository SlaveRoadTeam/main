using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    /// <summary>
    /// �J�ڐ�̃V�[����
    /// </summary>
    [SerializeField]
    string sceneChanageName = null;

    /// <summary>
    /// ���O�̃V�[����
    /// </summary>
    protected static string BeforeSceneName = "Title";

    /// <summary>
    /// ��b�p�[�g�𐧌䂷��R���|�[�l���g
    /// </summary>
    [SerializeField]
    NovelMessageController novelSystem = default;

    /// <summary>
    /// �V�[���ύX�ɂ�����x������
    /// </summary>
    [SerializeField]
    float sceneChanageDelayTime = 2.0f;

    /// <summary>
    /// �Ó]�p�摜�𐧌䂷��Animation
    /// </summary>
    [SerializeField]
    Animation blinderAnim = default;

    /// <summary>
    /// �O�̃V�[���֖߂�
    /// </summary>
    public void BackScene()
    {
        SceneManager.LoadScene(BeforeSceneName);
    }

    /// <summary>
    /// �w��̃V�[����
    /// </summary>
    public void GoScene(string sceneName)
    {
        BeforeSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    void Update()
    {
        //��b���V�X�e�������ׂĎ��s���I������V�[����ύX
        if(novelSystem && novelSystem.IsRunAllActions)
        {
            StartCoroutine(GoSceneLateTime());
        } 
    }

    /// <summary>
    /// �V�[���ύX��x�����s
    /// </summary>
    /// <returns></returns>
    IEnumerator GoSceneLateTime()
    {
        float delayTime = sceneChanageDelayTime;

        if (blinderAnim)
        {
            delayTime = Mathf.Max(delayTime - blinderAnim.clip.length, 0.001f);

            yield return new WaitForSeconds(blinderAnim.clip.length);

            blinderAnim.Play();
        }

        yield return new WaitForSeconds(delayTime);

        GoScene(sceneChanageName);
    }
}
