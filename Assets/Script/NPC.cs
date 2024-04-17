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


    /// <summary>
    /// NPC의 업데이트
    /// </summary>
    public void Updated()
    {

    }
}
