using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateGUI : MonoBehaviour
{
	public SingleFollower[] loadedData;

	public UnityEngine.UI.Text Username;
	public UnityEngine.UI.Text RealName;
	public UnityEngine.UI.Text Description;
	public UnityEngine.UI.Text Followers;
	public UnityEngine.UI.Text Following;
	//	public GUI
	// Use this for initialization
	void Start()
	{
//		Username = GameObject.Find("conts-Username").GetComponent<>;
//		RealName = GameObject.Find("conts-RealName").GetComponent<GUIText>().text;
//		Description = GameObject.Find("conts-Description").GetComponent<GUIText>().text;
//		Followers = GameObject.Find("conts-Followers").GetComponent<GUIText>().text;
//		Following = GameObject.Find("conts-Following").GetComponent<GUIText>().text;
	}
	
	// Update is called once per frame
	void Update()
	{
		
	}


	public void Populate(string name)
	{
		loadedData = GameObject.Find("Network").GetComponent<GetTwitter>().loadedData;
		print("populating " + name);

		for (int i = 0; i < loadedData.Length; i++)
		{
			if (name == loadedData[i].screen_name)
			{
				print("Found at " + i);
				SingleFollower picked = loadedData[i];
//				Debug.Log(loadedData[i].name + "\n" + loadedData[i].description + "\n" + loadedData[i].followers_count);
				Following.text = picked.friends_count;
				Followers.text = picked.followers_count;
				Description.text = picked.description;
				Username.text = picked.screen_name;
				RealName.text = picked.name;
			}
//			else
//			{
//				print("didn't find " + name);
//				for (int j = 0; j < loadedData.Length; j++)
//				{
//					Debug.Log(loadedData[j].screen_name);
//				}
//			}
		}

	}

}
