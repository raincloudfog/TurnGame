using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    int id;
    //NPC의 대화 데이터들
    [SerializeField]
    NPCData data;

    //NPC가 가지고있는 퀘스트들
    [SerializeField]
    Quest quest;

    //대화순서
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
        Debug.Log("나중에 이 함수를 전달하면서 대화 시스템 작동");

    }

    /*public string GetTalk()
    {
        // 대화 마지막 순서면 마지막 대화만 반복
        if (data.Talks[data.npcIntData.order].Talks.Length
            <= data.npcIntData.order)
        {
            int Length = data.Talks[data.npcIntData.order].Talks.Length - 1;
            return data.Talks[Length].Talks[conversationOrder];
        }

        // 대화 끝나면 
        if (data.Talks[data.npcIntData.order].Talks.Length
             >= conversationOrder)
        {
            conversationOrder = 0;
            data.npcIntData.order++;
        }

        return data.Talks[data.npcIntData.order].Talks[conversationOrder];
    }*/

    /// <summary>
    /// NPC의 업데이트
    /// </summary>
    public void Updated()
    {

    }
}
