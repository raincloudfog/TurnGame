using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public static TextManager instance;
   
    [SerializeField]
    TMP_Text TalkText;

    [SerializeField]
    GameObject TalkCanvas;



    private void Awake()
    {
        instance = this;
    }


    //���߿� ��ȭ ���� �ƴϿ� �־�� �� �����

    public void CreateDialogue(string Text)
    {
        TalkText.text = Text;
    }
}
