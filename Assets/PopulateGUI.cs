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
	public UnityEngine.UI.Image Photo;
	string fullResPhoto;


	//	public GUI
	// Use this for initialization
	void Start()
	{
//		defaulter = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f); 
	}
	
	// Update is called once per frame
	void Update()
	{
		
	}


	public void Populate(string name)
	{
		loadedData = GameObject.Find("Network").GetComponent<GetTwitter>().loadedData;
		if (name != "")
		{
			//print("populating " + name);

			for (int i = 0; i < loadedData.Length; i++)
			{
				if (name == loadedData[i].screen_name)
				{
//				print("Found at " + i);
					SingleFollower picked = loadedData[i];
//				Debug.Log(loadedData[i].name + "\n" + loadedData[i].description + "\n" + loadedData[i].followers_count);
					Following.text = "FOLLOWING: " + picked.friends_count;
					Followers.text = "FOLLOWERS: " + picked.followers_count;
					Description.text = "BIO: " + picked.description;
					Username.text = "@" + picked.screen_name;
					RealName.text = "NAME: " + picked.name;
					fullResPhoto = picked.profile_image_url.Replace("_normal", "");
//					print(fullResPhoto);
					StartCoroutine(GetPhoto(fullResPhoto));
				}
			}
		}
		else
		{
			Following.text = "FOLLOWING:";
			Followers.text = "FOLLOWERS:";
			Description.text = "BIO:";
			Username.text = "@";
			RealName.text = "NAME:";
			StartCoroutine(GetPhoto(""));

		}
	}

	public IEnumerator GetPhoto(string url)
	{
		// Start a download of the given URL
		if (url != "")
		{
			WWW www = new WWW(url);
			yield return www;

			Texture2D tex = www.texture;
			Sprite photoSprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f); 
			Photo.GetComponent<Image>().overrideSprite = photoSprite;
			Photo.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.3f);
		}
		else
		{ 
			Photo.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
		}
	}

}


