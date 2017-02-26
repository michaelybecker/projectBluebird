using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayc : MonoBehaviour {


    Camera camera;
	// Use this for initialization
	void Start () {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = camera.ScreenPointToRay(new Vector3(x, y));

            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red, 1);
            

        }
    }
}
