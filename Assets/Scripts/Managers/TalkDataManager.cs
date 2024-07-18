using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class TalkDataManager : MonoBehaviour
{
    string newDataPath = "";
    string useDataPath = "";
    string newDataName= "/talkLine.json";
    string useDataName= "/useTalkLine.json";

    private Dictionary<string, TalkData> talkDictionary = new Dictionary<string, TalkData>();
    TalkList talkList;

    public void Init()
    {
        newDataPath += Application.persistentDataPath + newDataName;
        useDataPath += Application.persistentDataPath + useDataName;
        if (!CheckDataPath(newDataPath))
        {
            TextAsset resourceFile = Resources.Load<TextAsset>("Data/" + newDataName.TrimStart('/').Replace(".json", ""));
            if (resourceFile != null)
                File.WriteAllText(newDataPath, resourceFile.text);
        }
        if (!CheckDataPath(useDataPath))
        {
            TextAsset resourceFile = Resources.Load<TextAsset>("Data/" + useDataName.TrimStart('/').Replace(".json", ""));
            if (resourceFile != null)
                File.WriteAllText(useDataPath, resourceFile.text);
        }

        //string newData = File.ReadAllText(newDataPath);
        //talkList = JsonUtility.FromJson<TalkList>(newData);
        //foreach (var talkVal in talkList.talks)
        //{
        //    if (!talkDictionary.ContainsKey(talkVal.talkerName))
        //    {
        //        talkDictionary.Add(talkVal.talkerName, talkVal);
        //    }
        //}
    }

    #region Relate Data : New, Continue, Save
    public void StartNew()
    {
        string newData = File.ReadAllText(newDataPath);
        talkList = JsonUtility.FromJson<TalkList>(newData);
        foreach (var talkVal in talkList.talks)
        {
            if (!talkDictionary.ContainsKey(talkVal.talkerName))
            {
                talkDictionary.Add(talkVal.talkerName, talkVal);
            }
        }
        // 기존 데이터 초기화
        string modifyString = JsonUtility.ToJson(talkList);
        File.WriteAllText(useDataPath, modifyString);
    }

    public void StartContinue()
    {
        string useData = File.ReadAllText(useDataPath);
        talkList = JsonUtility.FromJson<TalkList>(useData);
        foreach (var talkVal in talkList.talks)
        {
            if (!talkDictionary.ContainsKey(talkVal.talkerName))
            {
                talkDictionary.Add(talkVal.talkerName, talkVal);
            }
        }
    }

    public void SaveJson()
    {
        string modifyString = JsonUtility.ToJson(talkList);
        File.WriteAllText(useDataPath, modifyString);
    }

    public bool CheckDataPath(string _path)
    {
        if (File.Exists(_path))
            return true;
        return false;
    }

    public TalkData LoadTalk(string _name)
    {
        if (talkDictionary.ContainsKey(_name))
            return talkDictionary[_name];
        return null;
    }
    #endregion
}

[System.Serializable]
public class TalkData
{
    public bool isTalk;
    public string talkerName;
}

[System.Serializable]
public class TalkList
{
    public List<TalkData> talks; // Json Name
}
