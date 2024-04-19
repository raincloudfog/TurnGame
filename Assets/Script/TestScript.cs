using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Ű �Է¿� ���� ��������Ʈ ����
    public delegate void KeyPressedHandler(KeyCode key);
    public event KeyPressedHandler OnKeyPressed;


    // �̺�Ʈ�� ����� Ű ��ư
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
