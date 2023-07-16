using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump1 : MonoBehaviour
{
    bool jDown;

    Rigidbody rigid;
    Vector3 moveVec;

    void Start()
    {
        rigid = GetComponent<Rigidbody>(); 
    }

    void Update()
    {
        Jump();
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigid.AddForce(Vector3.up * 15, ForceMode.Impulse);
        }
    }
}