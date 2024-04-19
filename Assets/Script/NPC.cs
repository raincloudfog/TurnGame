using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    int id;
    //NPC�� ��ȭ �����͵�
    [SerializeField]
    NPCData data;

    //NPC�� �������ִ� ����Ʈ��
    [SerializeField]
    Quest quest;

    //��ȭ����
    int conversationOrder;

    // Start is called before the first frame update
    void Start()
    {
        data = TextLibrary.GetNPCText(id);
        quest = QuestLibrary.GetQuest(id);
    }

    public void Interaction()
    {
        GameState.instance.ChangeKey(GameState.ekeyState.TalkKey);
        UIManager.Instance.Talk(data);
        Debug.Log("���߿� �� �Լ��� �����ϸ鼭 ��ȭ �ý��� �۵�");

    }

    /*public string GetTalk()
    {
        // ��ȭ ������ ������ ������ ��ȭ�� �ݺ�
        if (data.Talks[data.npcIntData.order].Talks.Length
            <= data.npcIntData.order)
        {
            int Length = data.Talks[data.npcIntData.order].Talks.Length - 1;
            return data.Talks[Length].Talks[conversationOrder];
        }

        // ��ȭ ������ 
        if (data.Talks[data.npcIntData.order].Talks.Length
             >= conversationOrder)
        {
            conversationOrder = 0;
            data.npcIntData.order++;
        }

        return data.Talks[data.npcIntData.order].Talks[conversationOrder];
    }*/

    /// <summary>
    /// NPC�� ������Ʈ
    /// </summary>
    public void Updated()
    {

    }
}
