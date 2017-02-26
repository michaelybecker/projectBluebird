using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySpheres : MonoBehaviour {
    GetTwitter getTwitter;
	// Use this for initialization
	void Start () {
	    	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.D))
        {
            Debug.Log("triggered");
           // var testcall = "tessssssssssssssst";
            var spheres = GameObject.FindGameObjectsWithTag("tweetSphere");
            foreach (GameObject sphere in spheres)
            {
              //  Debug.Log("sphere");
                Destroy(sphere);
            }
           // createSphereFromNodes = GameObject.Find("SphereCenter").GetComponent<CreateSphereFromNodes>();
            getTwitter = GameObject.Find("Network").GetComponent<GetTwitter>();
            getTwitter.Init("50");
         //   GameObject.Find("SphereCenter").SetActive(false);
          //  GameObject.Find("SphereCenter").SetActive(true);
        }
    }
}
