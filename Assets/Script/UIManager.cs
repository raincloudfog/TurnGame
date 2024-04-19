using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    [SerializeField]
    TalkCanvas talkCanvas;
   

    private void Awake()
    {
        Instance = this;
    }

    public void Talk(NPCData npc)
    {
        Debug.Log("UIManager:: Talk(NPCData npc) : 1");
        //�̷��� ���� Ȯ���� ������ �ϱ⵵ �ϴ� ���� ��� �������� �����ؾ� �� �� ����.
        if(Game.Instance.gameinfo.npc == null)
        {
            Debug.Log("UIManager:: Talk(NPCData npc) : npc == "  + npc);
            Game.Instance.gameinfo.npc = npc;
        }
        talkCanvas.gameObject.SetActive(true);
        talkCanvas.CreateDialog(Game.Instance.gameinfo.npc);
        Debug.Log("UIManager:Talk():: ��ũ ��� �ؾߵ�");
    }
}
