using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }


    //게임 정보
    public Gameinfo gameinfo;

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

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameinfo = new Gameinfo();
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
