using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreTweet;

public class SampleTweetRetrieval : MonoBehaviour
{
	public UnityEngine.UI.InputField inputField;
	OAuth.OAuthSession s;
	Tokens tokens;
	// Use this for initialization
	void Start()
	{
		s = OAuth.Authorize("xEzMHunNVWKJt6aVMUyxVt9yU", "lNTocQbGjfK4xRjmIDnRh155IHpnPaVSHQBlROel3UUngVBDWS");
//		print(s.AuthorizeUri);
		string target = s.AuthorizeUri.ToString();
		Application.OpenURL(target);
	}
	
	// Update is called once per frame
	void Update()
	{
		
	}

	public void getTokens()
	{
		var pin = inputField.text;
		print("pin: " + pin);
		tokens = OAuth.GetTokens(s, pin);
//		print(tokens);
		var result = tokens.Friends.List(screen_name: "michaelhazani", count: 200);
		print(result);
	}
}
