using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignGuiToVR : MonoBehaviour
{
    public Camera cam;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cam.transform.position);
        transform.Rotate(0f, 180f, 0f);
        transform.position = cam.transform.position + cam.transform.rotation * Vector3.forward * 2.0f;
        //transform.rotation = Quaternion.Euler(cam.transform.eulerAngles.x, 0, 0);
    }
}
