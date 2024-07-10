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

    public void ApplyEnhanceData(EnhanceType _enhanceType)
    {
        switch (_enhanceType)
        {
            case EnhanceType.HP:
                for (int i = 0; i < 3; i++)
                {
                    datas[i].maxHp += enhanceData.increaseHpValue[enhanceData.hpDegree];
                }
                enhanceData.hpDegree += 1;
                break;
            case EnhanceType.Roll:
                for (int i = 0; i < 3; i++)
                {
                    datas[i].rollCoolTime -= enhanceData.increaseRollValue[enhanceData.rollDegree];
                }
                enhanceData.rollDegree += 1;
                break;
            case EnhanceType.Attack:
                for (int i = 0; i < 3; i++)
                {
                    datas[i].attackDamageValue += enhanceData.increaseHpValue[enhanceData.attackDegree];
                }
                enhanceData.attackDegree += 1;
                break;
            case EnhanceType.Buff:
                for (int i = 0; i < 3; i++)
                {
                    datas[i].buffSkillValue += enhanceData.increaseHpValue[enhanceData.buffDegree];
                }
                enhanceData.buffDegree += 1;
                break;
        }
    }

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

    public void ApplyEnhanceStat(EnhanceType _type)
    {
        int datasCnt = datas.Length;

        switch (_type)
        {
            case EnhanceType.HP:
                for (int idx = 0; idx < datasCnt; idx++)
                {
                    datas[idx].maxHp += enhanceData.increaseHpValue[enhanceData.hpDegree];
                }
                enhanceData.hpDegree += 1;
                break;
            case EnhanceType.Roll:
                for (int idx = 0; idx < datasCnt; idx++)
                {
                    datas[idx].rollCoolTime -= enhanceData.increaseRollValue[enhanceData.rollDegree];
                }
                enhanceData.rollDegree += 1;
                break;
            case EnhanceType.Attack:
                for (int idx = 0; idx < datasCnt; idx++)
                {
                    datas[idx].attackDamageValue += enhanceData.increaseAttackValue[enhanceData.attackDegree];
                }
                enhanceData.attackDegree += 1;
                break;
            case EnhanceType.Buff:
                for (int idx = 0; idx < datasCnt; idx++)
                {
                    datas[idx].buffSkillValue += enhanceData.increaseBuffValue[enhanceData.buffDegree];
                }
                enhanceData.buffDegree += 1;
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

    public float[] costs = { 3f, 4f, 5f };

    // max
    public int enhanceHpDegree = 3;
    public int enhanceRollDegree = 3;
    public int enhanceAttackDegree = 3;
    public int enhanceBuffDegree = 3;
}
