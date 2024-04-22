using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class TalkCanvas : MonoBehaviour
{

    
    [SerializeField, UnityEngine.Tooltip("대화 상자안에 텍스트")]    
    TMP_Text TalkText;

    //대화순서
    [SerializeField]
    int conversationOrder;

    /// <summary>
    /// 대화 끝
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

        

        // 대화 끝나면 대화 순서를 증가시키고 대화 내용을 초기화
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
        // 대화 마지막 순서면 마지막 대화만 반복
        if (data.Talks.Talks[data.npcIntData.order].Length
            <= data.npcIntData.order)
        {
            int Length = data.Talks.Talks[data.npcIntData.order].Length - 1;
            return data.Talks.Talks[Length][conversationOrder];
        }

        // 대화 끝나면 
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


#region 제이슨 테스트용도

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