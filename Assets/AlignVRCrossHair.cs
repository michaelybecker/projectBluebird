using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignVRCrossHair : MonoBehaviour {
    public Camera cam;
    public bool hit;
    public Vector3 hitPos;
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(cam.transform.position);
        transform.Rotate(cam.transform.rotation.x, 90.0f, 90f);
        transform.position = cam.transform.position + cam.transform.rotation * Vector3.forward * 6.0f;
        if (!(hit)) {

            //transform.localScale *= hitPos.z * 3;
        }
        else
        {
        //transform.position = hitPos;
            //transform.localScale *= hitPos.z / 3;
        }
        //transform.rotation = Quaternion.Euler(cam.transform.eulerAngles.x, 0, 0);
    }
}
