using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // 키 입력에 대한 델리게이트 정의
    public delegate void KeyPressedHandler(KeyCode key);
    public event KeyPressedHandler OnKeyPressed;


    // 이벤트를 등록할 키 버튼
    public KeyCode keyToTrack;

    public KeyCode[] keyCodes = new KeyCode[]
    {
        KeyCode.Escape,
        KeyCode.Space,
        KeyCode.Z,
        KeyCode.X,
        KeyCode.C,
        KeyCode.V,

    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vec;

        vec.x = Input.GetAxis("Horizontal");
        vec.y = Input.GetAxis("Vertical");
        vec.z = 0;
        
        Debug.Log($"TestScript::vec : ({vec.x}, {vec.y}, {vec.z})");

        if(Input.GetKeyDown(keyToTrack))
        {
            Debug.Log("keyToTrack : " + keyToTrack);
            OnKeyPressed?.Invoke(keyToTrack);
        }

        
    }
}
