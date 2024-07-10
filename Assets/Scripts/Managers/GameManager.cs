using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    #region Not refer Managers
    private LoadSceneManager loadScene_Manager = new LoadSceneManager();
    public static LoadSceneManager LoadScene_Manager { get { return Instance.loadScene_Manager; } }
    #endregion

    #region Refer Managers
    [SerializeField] PlayerDataManager pDataManager;
    public PlayerDataManager PDataManager
    { 
        get
        {
            if (pDataManager == null)
                pDataManager = GetComponent<PlayerDataManager>();
            return pDataManager; 
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
