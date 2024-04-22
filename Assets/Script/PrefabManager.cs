using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : Singleton<PrefabManager>
{
    public Dictionary<int , GameObject> prefabDictionary = new Dictionary<int , GameObject>();

    public void AddPrefab(int id , GameObject obj)
    {
        if(!prefabDictionary.ContainsKey(id))
        {
            prefabDictionary.Add(id, obj);
        }
        else
        {
            Debug.Log("�̹� �ش� id�� ������ �ֽ��ϴ�.");
        }
    }

    public GameObject GetPrefab(int id)
    {
        GameObject obj = prefabDictionary[id];
        if(obj == null)
        {
            return null;
        }
        return obj;
    }
    
}
