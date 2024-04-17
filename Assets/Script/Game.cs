using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    // 게임 정지
    [SerializeField]
    bool gamePulse;

    [SerializeField]
    List<NPC> NPCs = new List<NPC>();

    [SerializeField]
    Unit Player;
    public void GamePulse(bool Pulse)
    {
        gamePulse = Pulse;
    }

    public void AddNPC(NPC npc)
    {
        if(!NPCs.Contains(npc))
        {
            NPCs.Add(npc);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gamePulse == true)
        {
            return;
        }

        Player.Updated();

        foreach (NPC npcs in NPCs)
        {
            npcs.Updated();
        }


    }
}
