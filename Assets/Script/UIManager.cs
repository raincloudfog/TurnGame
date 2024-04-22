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
        //�̷��� ���� Ȯ���� ������ �ϱ⵵ �ϴ� ���� ��� �������� �����ؾ� �� �� ����.
        if(Game.Instance.gameinfo.npc == null || Game.Instance.gameinfo.npc != npc)
        {
            Game.Instance.gameinfo.ChangeNpc(npc);
        }
        talkCanvas.gameObject.SetActive(true);
        if(!talkCanvas.CreateDialog(Game.Instance.gameinfo.npc))
        {
            talkCanvas.ExitGameObj();
        }
    }
}
