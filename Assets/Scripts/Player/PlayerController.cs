using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] PlayerData playerData;
    //[SerializeField] float xSpeed;
    //[SerializeField] float ySpeed;

    PlayerType currentType;
    
    NormalPlayer normalPlayer;

    #region Unity Cycle
    private void Awake()
    {
        currentType = PlayerType.Normal;
        normalPlayer = GetComponentInChildren<NormalPlayer>();
        if (normalPlayer != null)
            normalPlayer.Init(this);
    }

    private void Update()
    {
        if (normalPlayer != null)
        {
            normalPlayer.Execute();
            return;
        }
    }
    #endregion
}
