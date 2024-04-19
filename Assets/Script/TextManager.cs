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


    //나중에 대화 수락 아니오 넣어보는 건 고려중

    public void CreateDialogue(string Text)
    {
        TalkText.text = Text;
    }
}
