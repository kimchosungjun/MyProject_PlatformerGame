using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerType currentType;

    [SerializeField] NormalPlayer normalPlayer;

    #region Unity Cycle
    private void Awake()
    {
        currentType = PlayerType.Normal;
        if (normalPlayer != null)
            normalPlayer.Init(this);
    }

    private void Update()
    {
        if (normalPlayer != null)
        {
            // ���Ŀ� �ٸ� ���� Ÿ�� �߰� ��, Switch������ �б� ���� �����Ű��
            normalPlayer.Execute();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // ���Ŀ� ���� ���� ��, �ڵ� �ۼ�
            // ���� ��� �������� ȣ�� ����
        }
    }
    #endregion
}
