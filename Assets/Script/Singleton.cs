using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> :MonoBehaviour where T : MonoBehaviour
{
    static T instance;

    public static T Instance
    { get 
        {
            if(instance == null)
            {
                T obj = GameObject.Find(typeof(T).Name) as T;
                if(obj != null)
                {
                    instance = obj;
                }

                if(obj == null)
                {
                    GameObject newobj = new GameObject(typeof(T).Name);
                    instance = newobj.AddComponent<T>();
                }
                DontDestroyOnLoad(instance);
            }

            return instance; 
        } 
        set { instance = value; }
    }

    public virtual void Init()
    {

    }
}
