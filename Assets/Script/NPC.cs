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

    // Start is called before the first frame update
    void Start()
    {
        data = TextLibrary.GetNPCText(id);
        quest = QuestLibrary.GetQuest(id);
    }

    public void Interaction()
    {
        Game.Instance.ChangeKey(Game.ekeyState.TalkKey);
        UIManager.Instance.Talk(data);
        Debug.Log("나중에 이 함수를 전달하면서 대화 시스템 작동");

    }
    
    /// <summary>
    /// NPC의 업데이트
    /// </summary>
    public void Updated()
    {

    }
}
