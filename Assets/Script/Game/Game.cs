using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BasicKey;

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

    public enum ekeyState
    {
        BasicKey,
        TalkKey
    }


    [SerializeField]
    KeyState[] keyStates;
    [SerializeField]
    KeyState keyState;

    KeyCode newkey;

    private void Awake()
    {
        Instance = this;
        Init();
    }

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

    public virtual void Init()
    {
        keyState = new BasicKey(Player);
        keyStates = new KeyState[]
        {
            new BasicKey(Player)
            , new TalkKey(Player)
        };
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

        if (Input.anyKey)
        {
            newkey = DetectNewKey();
            keyState.GetKey(newkey);
        }

        if (Input.anyKeyDown)
        {
            newkey = DetectNewKey();
            keyState.GetKeyDown(newkey);
        }


    }
    

    KeyCode DetectNewKey()
    {
        foreach (KeyCode keycode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(keycode))
            {
                return keycode;
            }
        }

        return KeyCode.None;
    }


    /// <summary>
    /// 키 타입 바꾸기
    /// </summary>
    public void ChangeKey(ekeyState ekey)
    {
        keyState.Exit();
        keyState = keyStates[(int)ekey];
    }
}

//2024-04-19
//나중에 플레이 정보가 담긴 클래스 혹은 구조체 만들면 좋을 것같음.
/// <summary>
/// 현재 게임 정보를 담고 있기
/// </summary>
/// 
[Serializable]
public class Gameinfo
{
    //현재 대화 하고 있는 NpcData
    //대화용도의 정보만 가져올 용도
    public NPCData npc;

    public void ClearInfo()
    {

    }

    public void ClearNpc()
    {
        npc = null;
    }

    public void ChangeNpc(NPCData npc)
    {
        this.npc = npc;
    }
}

[Serializable]
public class KeyState
{
    protected KeyCode[] key;

    public Unit unit;

    //클래스 내부에서 사용할 transform과 direction
    [SerializeField]
    protected Vector2 charactorPosition;
    [SerializeField]
    protected Vector2 charactorDirection;

    [SerializeField]
    protected bool isKeyPressed = false; // 키가 눌린 상태인지 확인용

    public KeyState(Unit unit)
    {
        this.unit = unit;
    }

    public virtual void Exit()
    {

    }

    public virtual void SetUnit(Unit unit)
    {

    }

    public virtual void GetKey(KeyCode key)
    {

    }

    public virtual void GetKeyDown(KeyCode key)
    {

    }
}


/// <summary>
/// 일반 게임에서 사용되는 키
/// </summary>
[Serializable]
public class BasicKey : KeyState
{
    float distance = 1;
    LayerMask interactiontarget = LayerMask.GetMask("target");

    /*//클래스 내부에서 사용할 transform과 direction
    Vector2 charactorPosition;*/
    /*[SerializeField]
    Vector2 charactorDirection;*/

    public BasicKey(Unit unit) : base(unit)
    {
    }

    public override void Exit()
    {
        base.Exit();
        isKeyPressed = false;
    }

    public override void SetUnit(Unit unit)
    {
        this.unit = unit;
    }

    public override void GetKey(KeyCode key)
    {
        base.GetKey(key);
        unit.Move(Move(key));
    }

    public override void GetKeyDown(KeyCode key)
    {
        Interection(key);
    }

    /// <summary>
    /// 게임 캐릭터 움직임
    /// </summary>
    /// <param name="keyCode"></param>
    /// <returns></returns>
    public Vector3 Move(KeyCode keyCode)
    {

        Vector3 p;
        p.x = keyCode == KeyCode.RightArrow ?
            1 : keyCode == KeyCode.LeftArrow ? -1 : 0;
        p.y = keyCode == KeyCode.UpArrow ?
            1 : keyCode == KeyCode.DownArrow ? -1 : 0;
        p.z = 0;
        Debug.Log("KeyState : " + p);
        return p;
    }

    public void Interection(KeyCode keyCode)
    {
        if (keyCode != KeyCode.Z || isKeyPressed)
        {
            return;
        }

        charactorPosition = unit.transform.position;
        charactorDirection = unit.Direction;

        RaycastHit2D hit = Physics2D.Raycast(
            charactorPosition, charactorDirection, distance, interactiontarget);
        if (hit.collider != null)
        {
            NPC target = hit.collider.GetComponent<NPC>();
            if (target != null)
            {
                Debug.Log("BasicKey:Intrection(KeyCode keyCode):: hit != null");
                target.Interaction();
            }
        }
        else
        {
            Debug.Log("BasicKey:Intrection(KeyCode keyCode):: hit == null");
        }
    }



    /// <summary>
    /// 대화상태 못움직임
    /// </summary>
    [Serializable]
    public class TalkKey : KeyState
    {

        public TalkKey(Unit unit) : base(unit)
        {
        }

        public override void SetUnit(Unit unit)
        {
            this.unit = unit;
        }

        public override void GetKeyDown(KeyCode key)
        {
            base.GetKey(key);
            NextTalk(key);
            //Interection(key);
        }

        void NextTalk(KeyCode key)
        {
            if (key == KeyCode.Z)
            {
                UIManager.Instance.Talk(Game.Instance.gameinfo.npc);
            }
        }
    }



    /*public Action Interection(KeyCode keyCode)
   {
       bool Checktarget = keyCode == KeyCode.Z ? true : false;

       if (!Checktarget)
       {
           return null;
       }

       charactorPosition = unit.transform.position;
       charactorDirection = unit.Direction;

       RaycastHit2D hit = Physics2D.Raycast(
           charactorPosition, charactorDirection, distance,interactiontarget);
       if (hit.collider != null)
       {
           NPC target =   hit.collider.GetComponent<NPC>();
           if (target != null)
           {
               return target.GetFuntion();
           }
       }

       return null;
   }*/
}
