using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
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
    }
}
