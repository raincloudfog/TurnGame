using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public static class TextLibrary
{
    public static NPCList npcList;

    private static string NPCTextDataPath = "Assets/Script/Json/NPCText.json";

    public static void Load()
    {
        if(npcList != null)
        {
            return;
        }

        string json = File.ReadAllText(NPCTextDataPath);
        npcList = JsonUtility.FromJson<NPCList>(json);

        Debug.Log(npcList.NPCs[0].Talks);
        Debug.Log(npcList.NPCs[0]);        
    }

    public static NPCData GetNPCText(int id)
    {
        if (npcList == null)
        {
            Load();
            Debug.Log(npcList.NPCs.Length);
        }

        // npcList가 null인지 확인
        if (npcList == null)
        {
            Debug.LogError("npcList is null!");
            return null;
        }

        // id에 해당하는 NPC가 있는지 확인
        if (id < 0 || id >= npcList.NPCs.Length)
        {
            Debug.LogError("Invalid NPC id: " + id);
            return null;
        }

        // id에 해당하는 NPC 가져오기
        NPCData npcdata = npcList.NPCs[id];

        // npcdata가 null이 아닌지 확인
        if (npcdata == null)
        {
            Debug.LogError("NPCData is null for id: " + id);
            return null;
        }

        // 대화가 있는지 확인
        if (npcdata.Talks == null)
        {
            Debug.LogError("Talks is null for NPC id: " + id);
            return null;
        }

        // 대화가 비어 있는지 확인
        if (npcdata.Talks.Count == 0)
        {
            Debug.LogError("No talks available for NPC id: " + id);
            return null;
        }

        // 대화가 존재하면 반환
        return npcdata;
    }
}

[Serializable]
public class NPCList
{
    public NPCData[] NPCs;
}

[System.Serializable]
public class NPCData
{
    public int id;

    public List<DialogueSet> Talks;
    //public string[][] Talks;
    //public string[] Talks;
    //public List<string[]> Talks;
    //public TalkData[] Talks;
    public NPCBoolData npcBoolData;
    public NPCIntData npcIntData;
}

/*//해당 부분을 그냥 클래스로 만들어서 배열값을 받는 것으로 도전해보기
//해당 부분은 제이슨에서 변수명이없어서 못 옮기는 것 같음. 공부용으로 남겨두기
[System.Serializable]
public class TalkData
{
    public string[] Talk;
}*/

[Serializable]
public class DialogueSet
{
    public List<string> dialogueLines;
}

[System.Serializable]
public class NPCBoolData
{
    public bool checkEnemy;
    public bool checkMoney;
}

[System.Serializable]
public class NPCIntData
{
    //현재 몇번째의 대화인지 확인
    public int order;

    //나중에 NPC체력별로 뭘 할 수  있는지 궁금해서 만들어둠.
    public int HP;
}