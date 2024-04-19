using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState instance;

    public Unit player;

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
        instance = this;
        Init();
    }

    public virtual void Init()
    {
        keyState = new BasicKey(player);
        keyStates = new KeyState[]
        {
            new BasicKey(player)
            , new TalkKey(player)
        };
    }

    public virtual void Update()
    {
        if (Input.anyKey)
        {
            newkey = DetectNewKey();
            RemapKey(newkey);            
        }
    }

    KeyCode DetectNewKey()
    {
        foreach (KeyCode keycode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if(Input.GetKey(keycode))
            {
                return keycode;
            }
        }

        return KeyCode.None;
    }

    void RemapKey(KeyCode keyCode)
    {
        keyState.GetKey(keyCode);
    }


    /// <summary>
    /// 키 타입 바꾸기
    /// </summary>
    public void ChangeKey(ekeyState ekey )
    {
        keyState = keyStates[(int)ekey];
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

    public KeyState(Unit unit)
    {
        this.unit = unit;
    }

    public virtual void SetUnit(Unit unit)
    {

    }

    public virtual void GetKey(KeyCode key)
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

    public override void SetUnit(Unit unit)
    {
        this.unit = unit;
    }

    public override void GetKey(KeyCode key)
    {
        base.GetKey(key);

        Interection(key);
        if(key == KeyCode.Z)
        {
            return;
        }
        unit.Move(Move(key));
        
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
        Debug.Log("KeyState : "+ p);
        return p;
    }

    public void Interection(KeyCode keyCode)
    {
        bool Checktarget = keyCode == KeyCode.Z ? true : false;

        //Debug.Log("BasicKey:Intrection(KeyCode keyCode):: 발동되어야함");
        if (!Checktarget)
        {
            return;
        }

        //Debug.Log("BasicKey:Intrection(KeyCode keyCode):: 발동되어야함 아래 부분");

        charactorPosition = unit.transform.position;
        charactorDirection = unit.Direction;

        //Debug.Log("BasicKey:Intrection(KeyCode keyCode):: chractorPosition = "+ charactorPosition);

        //Debug.Log("BasicKey:Intrection(KeyCode keyCode):: charactorDirection = " + charactorDirection);

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

    public override void GetKey(KeyCode key)
    {
        //Debug.Log("TalkKey:: GetKey(KeyCode key)");
        base.GetKey(key);        
        //Interection(key);
    }

    void NextTalk(KeyCode key)
    {
        bool CheckKey;

        CheckKey = key == KeyCode.Z ? true : false;
        if(!CheckKey)
        {
            return;
        }



        

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