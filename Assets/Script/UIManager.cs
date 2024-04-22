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
        //이러면 굳이 확인을 여러번 하기도 하니 좋은 방법 떠오르면 수정해야 될 거 같음.
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
