using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolName
{
    DamageText,
}

public class ObjectPool : Singleton<ObjectPool>
{
    [SerializeField]
    Dictionary<PoolName, Pool> poolDic = new Dictionary<PoolName, Pool>();
        

    public void PlusItem(PoolName itemName, GameObject item)
    {
        if (poolDic.ContainsKey(itemName))
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject Clone = Instantiate(item);
                Clone.SetActive(false);
                poolDic[itemName].PlusItem(Clone);
                //Debug.Log("PlusItem");
            }
        }
        else
        {
            Debug.Log("불리면 안돼는데..");
        }
    }

    public void SetItem(PoolName itemName, GameObject item)
    {
        if (poolDic.ContainsKey(itemName))
        {
            poolDic[itemName].SetItem(item);
        }
    }

    public GameObject GetItem(PoolName itemName, GameObject item)
    {
        if (!poolDic.ContainsKey(itemName))
        {
            GameObject Clone = Instantiate(item);
            Clone.SetActive(false);
            AddpoolDic(itemName, Clone);
            Debug.Log("아이템 없음");
        }

        Debug.Log("GetItem");
        GameObject obj = poolDic[itemName].GetItem();

        obj.SetActive(true);
        return obj;
    }

    public void AddpoolDic(PoolName itemName, GameObject item)
    {
        if(!poolDic.ContainsKey(itemName))
        {
            poolDic.Add(itemName, new Pool(item));
            PlusItem(itemName, item);
            Debug.Log("addpoolDic");
        }
        else
        {
            Debug.Log("딕셔너리에 해당 키가 있습니다.");
        }
    }
}
