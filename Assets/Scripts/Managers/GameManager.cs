using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    #region Managers
    private LoadSceneManager loadScene_Manager = new LoadSceneManager();
    public static LoadSceneManager LoadScene_Manager { get { return Instance.loadScene_Manager; } }
    #endregion

    #region Controllers
    [SerializeField] PlayerController controller;
    public PlayerController Controller{ get { return controller; } }
    #endregion
    public void Awake()
    {
        #region Set GameManager
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
    }

    public void InitManagers()
    {

    }

    public void ClearManagers()
    {

    }
}
