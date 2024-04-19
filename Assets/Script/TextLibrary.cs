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

        // npcList�� null���� Ȯ��
        if (npcList == null)
        {
            Debug.LogError("npcList is null!");
            return null;
        }

        // id�� �ش��ϴ� NPC�� �ִ��� Ȯ��
        if (id < 0 || id >= npcList.NPCs.Length)
        {
            Debug.LogError("Invalid NPC id: " + id);
            return null;
        }

        // id�� �ش��ϴ� NPC ��������
        NPCData npcdata = npcList.NPCs[id];

        // npcdata�� null�� �ƴ��� Ȯ��
        if (npcdata == null)
        {
            Debug.LogError("NPCData is null for id: " + id);
            return null;
        }

        // ��ȭ�� �ִ��� Ȯ��
        if (npcdata.Talks == null)
        {
            Debug.LogError("Talks is null for NPC id: " + id);
            return null;
        }

        // ��ȭ�� ��� �ִ��� Ȯ��
        if (npcdata.Talks.Count == 0)
        {
            Debug.LogError("No talks available for NPC id: " + id);
            return null;
        }

        // ��ȭ�� �����ϸ� ��ȯ
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

/*//�ش� �κ��� �׳� Ŭ������ ���� �迭���� �޴� ������ �����غ���
//�ش� �κ��� ���̽����� �������̾�� �� �ű�� �� ����. ���ο����� ���ܵα�
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
    //���� ���°�� ��ȭ���� Ȯ��
    public int order;

    //���߿� NPCü�º��� �� �� ��  �ִ��� �ñ��ؼ� ������.
    public int HP;
}