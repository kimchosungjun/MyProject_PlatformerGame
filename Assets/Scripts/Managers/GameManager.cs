using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private BaseScene currentScene = null;
    public BaseScene CurrentScene { get { return currentScene; } set { currentScene = value;} } 
    [SerializeField] VideoPlayer videoPlayer;
    public VideoPlayer Video { get { if (videoPlayer == null) videoPlayer = GetComponent<VideoPlayer>(); return videoPlayer; } }
    #region Not refer Managers
    private LoadSceneManager loadScene_Manager = new LoadSceneManager();
    public static LoadSceneManager LoadScene_Manager { get { return Instance.loadScene_Manager; } }

    private AumManager aum_Manager = new AumManager();
    public static AumManager Aum_Manager { get { return Instance.aum_Manager; } }
    #endregion

    #region Refer Managers
    [Header("Player Data Manager")]
    [SerializeField] PlayerDataManager playerData_Manager;
    public PlayerDataManager PlayerData_Manager
    { 
        get
        {
            if (playerData_Manager == null)
                playerData_Manager = GetComponent<PlayerDataManager>();
            return playerData_Manager; 
        }
    }

    [Header("Talk Data Manager")]
    [SerializeField] TalkDataManager talkData_Manager;
    public TalkDataManager TalkData_Manager
    {
        get
        {
            if (talkData_Manager == null)
                talkData_Manager = GetComponent<TalkDataManager>();
            return talkData_Manager;
        }
    }

    [Header("Dialogue Data Manager")]
    [SerializeField] DialogueManager dialogue_Manager;
    public DialogueManager Dialogue_Manager
    {
        get
        {
            if (dialogue_Manager == null)
                dialogue_Manager = GetComponent<DialogueManager>();
            return dialogue_Manager;
        }
    }

    [Header("Cursor Manager")]
    [SerializeField] CursorManager cursor_Manager;
    public CursorManager Cursor_Manager { get { return cursor_Manager; } }
    #endregion

    [Header("Sound Manager")]
    [SerializeField] SoundManager sound_Manager;
    public SoundManager Sound_Manager { get { return sound_Manager; } }
    #region Controllers
    [Header("컨트롤러")]
    [SerializeField] PlayerController controller;
    public PlayerController Controller
    {
        get 
        {
            if (controller == null)
            {
                GameObject go = GameObject.FindWithTag("Player");
                if (go == null)
                    return null;
                else
                    controller = go.GetComponent<PlayerController>();
            }
            return controller; 
        } 
    }

    [SerializeField] UIController ui_Controller;
    public UIController UI_Controller
    {
        get 
        {
            if (ui_Controller == null)
            {
                GameObject ui = GameObject.FindWithTag("UI");
                if (ui == null)
                    return null;
                ui_Controller = ui.GetComponent<UIController>();
            }
            return ui_Controller; 
        }
    }
    #endregion

    public void Awake()
    {
        #region Set Singleton
        if (Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        #endregion
        InitManagers();
    }

    public void InitManagers()
    {
        //Dialogue_Manager.Init();
        PlayerData_Manager.Init();
        TalkData_Manager.Init();
    }

    //public void ClearManagers()
    //{

    //}

    public void SaveAllData()
    {
        PlayerData_Manager.SaveJson(); 
        TalkData_Manager.SaveJson();
    }
}
