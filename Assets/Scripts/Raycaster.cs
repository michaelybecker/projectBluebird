using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
	SingleFollower[] loadedData;
	Camera cam;
	PopulateGUI populateGUI;
	// Use this for initialization
	void Start()
	{
		cam = GameObject.Find("FPSController").GetComponentInChildren<Camera>();
		populateGUI = transform.GetComponent<PopulateGUI>();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				Debug.Log(hit);
				if (hit.collider.tag == "tweetsphere")
				{
					print(hit.collider.name);

					populateGUI.Populate(hit.collider.name);

				}
			}
			else
			{
				print("empty");
				populateGUI.Populate("");
			}

		}

	}

}
