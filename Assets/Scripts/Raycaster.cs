using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
	SingleFollower[] loadedData;
	Camera cam;
	PopulateGUI populateGUI;
    Vector3 Cpos;
	string currentTarget;
    public GameObject crosshair;
    float origZ;
    public AlignVRCrossHair aVRcs;
	// Use this for initialization
	void Start()
	{
		cam = GameObject.Find("FPSController").GetComponentInChildren<Camera>();
		populateGUI = transform.GetComponent<PopulateGUI>();
        Cpos = crosshair.transform.position;
        origZ = crosshair.transform.position.z;
    }
	
	// Update is called once per frame
	void Update()
	{
		Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
		RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "tweetsphere")
            {
                aVRcs.hit = true;
                
                if (hit.collider.name != currentTarget)
                {
                    aVRcs.hitPos = hit.collider.transform.position;
                    currentTarget = hit.collider.name;
                    populateGUI.Populate(hit.collider.name);
                    //crosshair.transform.position.Set(Cpos.x + 30, Cpos.y + 30, hit.collider.transform.position.z);
                }
            }
        }
        else
        {
            aVRcs.hit = false;
            if (currentTarget != "")
            {
                currentTarget = "";
                //crosshair.transform.position.Set(Cpos.x, Cpos.y, origZ);
                populateGUI.Populate("");
            }
        }

//		}

	}

}
