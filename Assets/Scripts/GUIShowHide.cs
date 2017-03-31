using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIShowHide : MonoBehaviour
{
	bool isShowing = true;
	public GameObject GUI;
	// Use this for initialization
	void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update()
	{
		
		if (Input.GetKeyDown("`"))
		{
			isShowing = !isShowing;
//			print(isShowing);
			GUI.SetActive(isShowing);
		}
	}
}
