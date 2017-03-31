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
	public AlignVRCrossHair aVRcs;
	public GetTwitter getTwitter;
	public GameObject Teleporter;
	teleport tele;

	// Use this for initialization
	void Start()
	{
		cam = GameObject.Find("Camera").GetComponent<Camera>();
		populateGUI = transform.GetComponent<PopulateGUI>();
		Cpos = crosshair.transform.position;
		tele = Teleporter.GetComponent<teleport>();
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
				if (Input.GetButtonDown("Fire1"))
				{
					tele.startGTP(hit.collider.transform.position);
//					getTwitter.Init(hit.collider.name, hit.collider.transform.position);
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
