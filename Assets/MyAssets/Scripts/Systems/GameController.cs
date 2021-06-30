using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Q�[���̏��
/// </summary>
public enum GameState
{
    /// <summary>
    /// �n�܂��Ă��Ȃ�
    /// </summary>
    NotStart = 0,
    /// <summary>
    /// �Q�[���v���C��
    /// </summary>
    Playing = 1,
    /// <summary>
    /// �Q�[���N���A
    /// </summary>
    Clear = 2,
    /// <summary>
    /// �Q�[���I�[�o�[
    /// </summary>
    Gameover = 3
}

/// <summary>
/// �Q�[���̃N���A����A�Q�[���I�[�o�[��������{
/// </summary>
public class GameController : MonoBehaviour
{
    /// <summary>
    /// �V�[���ύX�R���|�[�l���g
    /// </summary>
    [SerializeField]
    SceneChanger sceneChanger = default;
    /// <summary>
    /// �Q�[���N���A���ɔ�ԃV�[����
    /// </summary>
    [SerializeField]
    string clearedSceneName = "";
    /// <summary>
    /// �Q�[���I�[�o�[���ɔ�ԃV�[����
    /// </summary>
    [SerializeField]
    string gameoverSceneName = "";

    
    //[SerializeField]
    /*���Ԑ���R���|�[�l���g��z�u*/


    /// <summary>
    /// �Q�[���̏��
    /// </summary>
    [SerializeField]
    GameState state = GameState.NotStart;



    public GameState State { get => state; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
