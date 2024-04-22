using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public int id;

    public TMP_Text _damagetext;

    public void Awake()
    {
        id = 1;
        //PrefabManager.instance.AddPrefab(id, this);
    }

    public void SetText(string text)
    {
        _damagetext.text = text;
    }
    
}
