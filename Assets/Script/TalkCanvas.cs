using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class TalkCanvas : MonoBehaviour
{

    
    [SerializeField, UnityEngine.Tooltip("��ȭ ���ھȿ� �ؽ�Ʈ")]    
    TMP_Text TalkText;

    //��ȭ����
    [SerializeField]
    int conversationOrder;

    /// <summary>
    /// ��ȭ ��
    /// </summary>
    public void ExitGameObj()
    {
        TalkText.text = "";
        conversationOrder = 0;
        gameObject.SetActive(false);
    }

    public bool CreateDialog(NPCData npc)
    {
        if (npc == null)
        {
            Debug.LogError("NPCData is null!");
            return false;
        }

        if (npc.Talks == null || npc.Talks.Count == 0)
        {
            Debug.LogError("DialogueSet is null or empty!");
            return false;
        }

        DialogueSet currentDialogueSet = npc.Talks[npc.npcIntData.order];

        if (currentDialogueSet == null || currentDialogueSet.dialogueLines == null || currentDialogueSet.dialogueLines.Count == 0)
        {
            Debug.LogError("DialogueSet or dialogueLines is null or empty!");
            return false;
        }

        

        // ��ȭ ������ ��ȭ ������ ������Ű�� ��ȭ ������ �ʱ�ȭ
        if (conversationOrder >= currentDialogueSet.dialogueLines.Count)
        {
            conversationOrder = 0;
            npc.npcIntData.order++;
            if (npc.npcIntData.order >= npc.Talks.Count)
            {
                npc.npcIntData.order = npc.Talks.Count - 1;
            }
            Game.Instance.ChangeKey(Game.ekeyState.BasicKey);
                return false;
        }
        else
        {
            TalkText.text = currentDialogueSet.dialogueLines[conversationOrder];
            conversationOrder++;
        }

        return true;
    }


    /*public string GetTalk()
    {
        // ��ȭ ������ ������ ������ ��ȭ�� �ݺ�
        if (data.Talks.Talks[data.npcIntData.order].Length
            <= data.npcIntData.order)
        {
            int Length = data.Talks.Talks[data.npcIntData.order].Length - 1;
            return data.Talks.Talks[Length][conversationOrder];
        }

        // ��ȭ ������ 
        if (data.Talks.Talks[data.npcIntData.order].Length
             >= conversationOrder)
        {
            conversationOrder = 0;
            data.npcIntData.order++;
        }

        return data.Talks.Talks[data.npcIntData.order][conversationOrder];
    }
*/
}


#region ���̽� �׽�Ʈ�뵵

/*public class TestData 
{
    TestA[] aa;
}

public class TestA
{
    public string[] teststring;
}
*/


/*{
    "aa": [
      [
        "a",
      "b",
      "c",
      "d"
    ],
    [
      "f"
    ],
    [
      "g"
    ]
  ]
}*/
#endregion