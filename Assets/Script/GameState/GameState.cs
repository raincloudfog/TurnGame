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
    /// Ű Ÿ�� �ٲٱ�
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

    //Ŭ���� ���ο��� ����� transform�� direction
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
/// �Ϲ� ���ӿ��� ���Ǵ� Ű
/// </summary>
[Serializable]
public class BasicKey : KeyState
{
    float distance = 1;
    LayerMask interactiontarget = LayerMask.GetMask("target");

    /*//Ŭ���� ���ο��� ����� transform�� direction
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
    /// ���� ĳ���� ������
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

        //Debug.Log("BasicKey:Intrection(KeyCode keyCode):: �ߵ��Ǿ����");
        if (!Checktarget)
        {
            return;
        }

        //Debug.Log("BasicKey:Intrection(KeyCode keyCode):: �ߵ��Ǿ���� �Ʒ� �κ�");

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
/// ��ȭ���� ��������
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
//���߿� �÷��� ������ ��� Ŭ���� Ȥ�� ����ü ����� ���� �Ͱ���.
/// <summary>
/// ���� ���� ������ ��� �ֱ�
/// </summary>
/// 
[Serializable]
public class Gameinfo
{
    //���� ��ȭ �ϰ� �ִ� NpcData
    //��ȭ�뵵�� ������ ������ �뵵
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