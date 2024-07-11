using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    #region Not refer Managers
    private LoadSceneManager loadScene_Manager = new LoadSceneManager();
    public static LoadSceneManager LoadScene_Manager { get { return Instance.loadScene_Manager; } }

    private AumManager aum_Manager = new AumManager();
    public static AumManager Aum_Manager { get { return Instance.aum_Manager; } }
    #endregion

    #region Refer Managers
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

 
    #endregion

    #region Controllers
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
                ui_Controller = ui.GetComponentInChildren<UIController>();
                if (ui_Controller == null)
                    return null;
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
      
    }

    public void ClearManagers()
    {

    }
}
