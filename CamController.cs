using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public GameObject player;
    public float xmove = 0;
    public float ymove = 0;
    public float distance = 3;
    public float SmoothTime = 0.2f;
    private Vector3 velocity = Vector3.zero;
    private int toggleView = 3;

 
    void Update()
    {
        if (Input.GetMouseButton(1)) //¿ìÅ¬¸¯
        {
            xmove += Input.GetAxis("Mouse X"); 
            ymove -= Input.GetAxis("Mouse Y"); 
        }
        transform.rotation = Quaternion.Euler(ymove*2, xmove*2, 0);

        if (Input.GetMouseButtonDown(2)) //ÈÙÅ¬¸¯
            toggleView = 4 - toggleView;

        if (toggleView == 1)
        {
            Vector3 reverseDistance = new Vector3(0.0f, 0.4f, 0.2f);
            transform.position = player.transform.position + transform.rotation * reverseDistance;
        }
        else if (toggleView == 3)
        {
            Vector3 reverseDistance = new Vector3(0.0f, 0.0f, distance);
            transform.position = player.transform.position - transform.rotation * reverseDistance;
        }
    }
}
