using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Pool
{
    //풀에 들어가있는 그냥 오브젝트
    GameObject item;
    [SerializeField]
    List<GameObject> list = new List<GameObject>();
    
    public Pool(GameObject item)
    {
        this.item = item;
        PlusItem(item);
    }

    public void PlusItem(GameObject item)
    {
            list.Add(item);
        Debug.Log(list.Count);
    }

    public GameObject GetItem()
    {
        if(list.Count == 0)
        {
            PlusItem(item);
        }

        GameObject giveItem = list[list.Count - 1];
        list.Remove(giveItem);

        return giveItem;
    }

    public void SetItem(GameObject obj)
    {
        list.Add(obj);
    }

    

}
