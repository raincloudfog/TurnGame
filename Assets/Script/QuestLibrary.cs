using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class QuestLibrary 
{
    public static QuestList questList;

    public enum NPCName
    {
        Vallegehead,
        Villagemalevillager,
    }

    private static string questDataPath = "Assets/Script/Json/Quest.json";

    public static void LoadQuestData()
    {
        string json = File.ReadAllText(questDataPath);
        questList = JsonUtility.FromJson<QuestList>(json);
    }

    /// <summary>
    /// NPC에게 퀘스트 주기
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static Quest GetQuest(int id)
    {
        if(questList == null)
        {
            LoadQuestData();
        }
        return questList.quests[id];    
    }
}

[Serializable]
public class Quest
{
    public int id;
    public string title;
    public string description;
    public string[] objectives;
    public Reward reward;
    public bool completed;
}

[Serializable]
public class Reward
{
    public int exp;
    public int gold;
}

[Serializable]
public class QuestList
{
    public Quest[] quests;
}
