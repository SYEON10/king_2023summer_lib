using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myball : MonoBehaviour
{
    // Start is called before the first frame update
     Rigidbody rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump")){
            rigid.AddForce(Vector3.up * 50, ForceMode.Impulse);
        }

        Vector3 vec = new Vector3 (Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        rigid.AddForce(vec,ForceMode.Impulse);
    }
}
