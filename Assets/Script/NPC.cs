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
        Debug.Log("���߿� �� �Լ��� �����ϸ鼭 ��ȭ �ý��� �۵�");

    }
    
    /// <summary>
    /// NPC�� ������Ʈ
    /// </summary>
    public void Updated()
    {

    }
}
