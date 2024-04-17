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
    }

    public static NPCData GetNPCText(int id)
    {
        if(npcList == null)
        {
            Load();
        }
        NPCData npcdata;
        npcdata = npcList.NPCs[id - 1];
        return npcdata;
    }
}

public class NPCList
{
    public NPCData[] NPCs;
}

[System.Serializable]
public class NPCData
{
    public int id;
    public TextData Talks;
    public NPCBoolData npcBoolData;
    public NPCIntData npcIntData;
}

[System.Serializable]
public class TextData
{
    public List<string[]> Talks;
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
    //���� ���°�� ��ȭ���� Ȯ��
    public int order;

    //���߿� NPCü�º��� �� �� ��  �ִ��� �ñ��ؼ� ������.
    public int HP;
}