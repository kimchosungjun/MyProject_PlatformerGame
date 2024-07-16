using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class TalkDataManager : MonoBehaviour
{
    [SerializeField, Tooltip("대화 데이터")] TextAsset talkLine;
    string path = Application.dataPath + "/Resources/TalkData/talkLine.json";
    private Dictionary<string, TalkData> talkDictionary = new Dictionary<string, TalkData>();
    TalkList talkList;

    public void Awake()
    {
        LoadJsonFile();
    }

    public void LoadJsonFile()
    {
        if (talkLine != null)
        {
            talkList = JsonUtility.FromJson<TalkList>(talkLine.text);
            foreach (var talk in talkList.talks)
            {
                if (!talkDictionary.ContainsKey(talk.talkerName))
                {
                    talkDictionary.Add(talk.talkerName, talk);
                }
            }
        }
    }

    public TalkData LoadTalk(string _ID)
    {
        if (talkDictionary.ContainsKey(_ID))
        {
            return talkDictionary[_ID];
        }
        return null;
    }

    public void SaveJson()
    {
        string modifyString = JsonConvert.SerializeObject(talkList, Formatting.Indented);
        File.WriteAllText(path, modifyString);
    }
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
