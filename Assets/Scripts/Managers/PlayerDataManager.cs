using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerDataManager : MonoBehaviour
{
    // PlayerData Value
    string newDataPath = "";
    string useDataPath = "";
    string newDataName = "/playerData.json";
    string useDataName = "/playerUseData.json";

    public string UseDataPath { get { return useDataPath; } }
    // Player Data 
    PlayerData pData;
    public PlayerData PData { get { return pData; } set { pData = value; } }
    
    // Enhance Data : Check can enhance
    EnhanceValueData enhanceData = new EnhanceValueData();
    public EnhanceValueData EnhanceData { get { return enhanceData; } }

    #region Relate Data : New, Continue, Save
    public void StartNew()
    {
        string newData = File.ReadAllText(newDataPath);
        pData = JsonUtility.FromJson<PlayerData>(newData);
        // 기존 데이터 초기화
        string modifyString = JsonUtility.ToJson(pData);
        File.WriteAllText(useDataPath, modifyString);
    }

    public void StartContinue()
    {
        string useData = File.ReadAllText(useDataPath);
        pData = JsonUtility.FromJson<PlayerData>(useData);
    }

    public void SaveJson()
    {
        string modifyString = JsonUtility.ToJson(pData);
        File.WriteAllText(useDataPath, modifyString);
    }

    public bool CheckDataPath(string _path)
    {
        if (File.Exists(_path))
            return true;
        return false;
    }
    #endregion

    #region Setting
    public void Init()
    {
        newDataPath = Application.persistentDataPath + newDataName;
        useDataPath = Application.persistentDataPath + useDataName;
        if (!CheckDataPath(newDataPath))
        {
            TextAsset resourceFile = Resources.Load<TextAsset>("Data/" + newDataName.TrimStart('/').Replace(".json", ""));
            if (resourceFile != null)
                File.WriteAllText(newDataPath, resourceFile.text);
        }
    }

    public void CreateUseData()
    {
        if (!CheckDataPath(useDataPath))
        {
            TextAsset resourceFile = Resources.Load<TextAsset>("Data/" + useDataName.TrimStart('/').Replace(".json", ""));
            if (resourceFile != null)
                File.WriteAllText(useDataPath, resourceFile.text);
        }
    }

    /// <summary>
    /// Call By Lobby Btn : When you start Game
    /// </summary>
    /// <param name="_isContinue"></param>
    public void LoadJson(bool _isContinue)
    {
        if(_isContinue)
        {
            StartContinue();
            return;
        }
        else
        {
            StartNew();
            return;
        }
    }
    #endregion

    #region Enhance
    public bool CanEnhance(EnhanceType _type)
    {
        switch (_type)
        {
            case EnhanceType.HP:
                if (enhanceData.enhanceHpMaxDegree > pData.curHpDegree)
                    return true;
                else
                    return false;
            case EnhanceType.Roll:
                if (enhanceData.enhanceRollMaxDegree > pData.curRollDegree)
                    return true;
                else
                    return false;
            case EnhanceType.Attack:
                if (enhanceData.enhanceAttackMaxDegree > pData.curAttackDegree)
                    return true;
                else
                    return false;
            case EnhanceType.Buff:
                if (enhanceData.enhanceBuffMaxDegree > pData.curBuffDegree)
                    return true;
                else
                    return false;
            default:
                return false;
        }
    }

    public void ApplyEnhanceStat(EnhanceType _type, EnhanceUI _enhanceUI)
    {
        switch (_type)
        {
            case EnhanceType.HP:
                pData.maxHP += enhanceData.increaseHpValue[pData.curHpDegree];
                GameManager.Instance.Controller.MaxHp = pData.maxHP;
                pData.curHpDegree += 1;
                break;
            case EnhanceType.Roll:
                pData.rollCoolTime -= enhanceData.increaseRollValue[pData.curRollDegree];
                pData.curRollDegree += 1;
                break;
            case EnhanceType.Attack:
                pData.damageValue += enhanceData.increaseAttackValue[pData.curAttackDegree];
                pData.curAttackDegree += 1;
                break;
            case EnhanceType.Buff:
                pData.buffSkillValue += enhanceData.increaseBuffValue[pData.curBuffDegree];
                pData.curBuffDegree += 1;
                break;
        }
    }
    #endregion
}


