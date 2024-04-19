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
        //이러면 굳이 확인을 여러번 하기도 하니 좋은 방법 떠오르면 수정해야 될 거 같음.
        if(Game.Instance.gameinfo.npc == null)
        {
            Debug.Log("UIManager:: Talk(NPCData npc) : npc == "  + npc);
            Game.Instance.gameinfo.npc = npc;
        }
        talkCanvas.gameObject.SetActive(true);
        talkCanvas.CreateDialog(Game.Instance.gameinfo.npc);
        Debug.Log("UIManager:Talk():: 토크 출력 해야됨");
    }
}
