using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follower : MonoBehaviour
{
    
    public Transform target;
    public Vector3 offset;
    //따라갈 목표와 위치 오프셋을 public 변수로 선언


    void Update()
    {
     transform.position = target.position + offset;   
    }
}
