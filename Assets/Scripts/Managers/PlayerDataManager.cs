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
    string dataPath = "qwer"; /* Application.persistentDataPath;*/
    string dataName = "/playerData";

    public void ApplyEnhanceData(EnhanceBtnType _enhanceType)
    {
        switch (_enhanceType)
        {
            case EnhanceBtnType.HP:
                for (int i = 0; i < 3; i++)
                {
                    datas[i].maxHp += enhanceData.increaseHpValue[enhanceData.hpDegree];
                }
                enhanceData.hpDegree += 1;
                break;
            case EnhanceBtnType.Roll:
                for (int i = 0; i < 3; i++)
                {
                    datas[i].rollCoolTime -= enhanceData.increaseRollValue[enhanceData.rollDegree];
                }
                enhanceData.rollDegree += 1;
                break;
            case EnhanceBtnType.Attack:
                for (int i = 0; i < 3; i++)
                {
                    datas[i].attackDamageValue += enhanceData.increaseHpValue[enhanceData.attackDegree];
                }
                enhanceData.attackDegree += 1;
                break;
            case EnhanceBtnType.Buff:
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
        // 이것도 안됨 에러생김
        string loadData = File.ReadAllText(dataPath+dataName);
        if (loadData == string.Empty)
            return false;
        return true;
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
        string loadData = File.ReadAllText(dataPath + dataName);
        enhanceData = JsonUtility.FromJson<EnhanceValueData>(loadData);
        int datasCnt = datas.Length;
        for (int idx = 0; idx < datasCnt; idx++)
        {
            datas[idx].Continue();
        }
    }
}

public class EnhanceValueData
{
    public int hpDegree = 0;
    public int rollDegree = 0;
    public int attackDegree = 0;
    public int buffDegree = 0;

    public float[] increaseHpValue = { 15f, 15f, 20f };
    public float[] increaseRollValue = { 1f, 1f, 2f };
    public float[] increaseAttackValue = { 0.5f, 0.5f, 1f};
    public float[] increaseBuffValue = { 0.1f, 0.1f, 0.2f };

    public float[] costs = { 3f, 4f, 5f };

    public int enhanceHpDegree = 0;
    public int enhanceRollDegree = 0;
    public int enhanceAttackDegree = 0;
    public int enhanceBuffDegree = 0;
}
