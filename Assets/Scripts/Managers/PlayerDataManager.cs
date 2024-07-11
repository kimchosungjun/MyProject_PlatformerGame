using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerDataManager : MonoBehaviour
{
    [Header("Data (0:Normal, 1:Wind, 2:Water)")]
    [SerializeField] PlayerData[] datas;
    EnhanceValueData enhanceData;
    public EnhanceValueData EnhanceData { get { return enhanceData; } }

    // Application 경로를 읽지 못하는 버그 => 다른걸로 수정 필요!
    string dataPath = Application.dataPath + "/Resources/PlayerData/playerData";
   
    public bool CheckDataPath()
    {
        if (File.Exists(dataPath))
            return true;
        return false;
    }

    public void StartNew()
    {
        enhanceData = new EnhanceValueData();
        string toStringData = JsonUtility.ToJson(enhanceData);
        File.WriteAllText(dataPath, toStringData);
        int datasCnt = datas.Length;
        for (int idx = 0; idx < datasCnt; idx++)
        {
            datas[idx].Init();
        }
    }

    public void StartContinue()
    {
        enhanceData = new EnhanceValueData();
        string loadData = File.ReadAllText(dataPath);
        enhanceData = JsonUtility.FromJson<EnhanceValueData>(loadData);
        int datasCnt = datas.Length;
        for (int idx = 0; idx < datasCnt; idx++)
        {
            datas[idx].Continue();
        }
        enhanceData.hpDegree = datas[0].curHpDegree;
        enhanceData.rollDegree = datas[0].curRollDegree;
        enhanceData.attackDegree = datas[0].curAttackDegree;
        enhanceData.buffDegree = datas[0].curBuffDegree;
    }

    public void SaveData()
    {
        string toStringData = JsonUtility.ToJson(enhanceData);
        File.WriteAllText(dataPath, toStringData);
    }

    public bool CanEnhance(EnhanceType _type)
    {
        switch (_type)
        {
            case EnhanceType.HP:
                if (enhanceData.hpDegree < enhanceData.enhanceHpDegree)
                    return true;
                else
                    return false;
            case EnhanceType.Roll:
                if (enhanceData.rollDegree < enhanceData.enhanceRollDegree)
                    return true;
                else
                    return false;
            case EnhanceType.Attack:
                if (enhanceData.attackDegree < enhanceData.enhanceAttackDegree)
                    return true;
                else
                    return false;
            case EnhanceType.Buff:
                if (enhanceData.buffDegree < enhanceData.enhanceBuffDegree)
                    return true;
                else
                    return false;
            default:
                return false;
        }
    }

    public void ApplyEnhanceStat(EnhanceType _type, EnhanceUI _enhanceUI)
    {
        int datasCnt = datas.Length;

        switch (_type)
        {
            case EnhanceType.HP:
                for (int idx = 0; idx < datasCnt; idx++)
                {
                    datas[idx].maxHp += enhanceData.increaseHpValue[enhanceData.hpDegree];
                    GameManager.Instance.Controller.MaxHp+= enhanceData.increaseHpValue[enhanceData.hpDegree];
                    datas[idx].curHpDegree += 1;
                }
                enhanceData.hpDegree += 1;
                if (!CanEnhance(_type))
                    _enhanceUI.UpdateBtnState(0, false);
                else
                    _enhanceUI.UpdateBtnState(0, true);
                break;
            case EnhanceType.Roll:
                for (int idx = 0; idx < datasCnt; idx++)
                {
                    datas[idx].rollCoolTime -= enhanceData.increaseRollValue[enhanceData.rollDegree];
                    datas[idx].curRollDegree += 1;
                }
                enhanceData.rollDegree += 1;
                if (!CanEnhance(_type))
                    _enhanceUI.UpdateBtnState(1, false);
                else
                    _enhanceUI.UpdateBtnState(1, true);
                break;
            case EnhanceType.Attack:
                for (int idx = 0; idx < datasCnt; idx++)
                {
                    datas[idx].attackDamageValue += enhanceData.increaseAttackValue[enhanceData.attackDegree];
                    datas[idx].curAttackDegree += 1;
                }
                enhanceData.attackDegree += 1;
                if (!CanEnhance(_type))
                    _enhanceUI.UpdateBtnState(2, false);
                else
                    _enhanceUI.UpdateBtnState(2, true);
                break;
            case EnhanceType.Buff:
                for (int idx = 0; idx < datasCnt; idx++)
                {
                    datas[idx].buffSkillValue += enhanceData.increaseBuffValue[enhanceData.buffDegree];
                    datas[idx].curBuffDegree += 1;
                }
                enhanceData.buffDegree += 1;
                if (!CanEnhance(_type))
                    _enhanceUI.UpdateBtnState(3, false);
                else
                    _enhanceUI.UpdateBtnState(3, true);
                break;
        }
    }
}

public class EnhanceValueData
{
    // current
    public int hpDegree = 0;
    public int rollDegree = 0;
    public int attackDegree = 0;
    public int buffDegree = 0;

    public float[] increaseHpValue = { 15f, 15f, 20f };
    public float[] increaseRollValue = { 1f, 1f, 2f };
    public float[] increaseAttackValue = { 0.5f, 0.5f, 1f};
    public float[] increaseBuffValue = { 0.1f, 0.1f, 0.2f };

    public int[] costs = { 3, 4, 5 };

    // max
    public int enhanceHpDegree = 3;
    public int enhanceRollDegree = 3;
    public int enhanceAttackDegree = 3;
    public int enhanceBuffDegree = 3;
}
