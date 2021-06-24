using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    /// <summary>
    /// ���O�̃V�[����
    /// </summary>
    protected static string BeforeSceneName = "Title";



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



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
