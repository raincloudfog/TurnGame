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

    // Start is called before the first frame update
    void Start()
    {
        data = TextLibrary.GetNPCText(id);
        quest = QuestLibrary.GetQuest(id);
    }


    /// <summary>
    /// NPC�� ������Ʈ
    /// </summary>
    public void Updated()
    {

    }
}
