using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;


//using UnityEngine.Events

public class GetTwitter : MonoBehaviour
{
	public SingleFollower[] loadedData;
	//ArrayList FollowersList = new ArrayList();
	public CreateSphereFromNodes createSphereFromNodes;
	//	public GameObject player;
	public FirstPersonController controller;
	public UnityEngine.UI.InputField inputField;
	public UnityEngine.UI.Button getFollowersBtn;
	public UnityEngine.UI.Button getFriendsBtn;
	public UnityEngine.UI.Button getFollowingBtn;
	public FirstPersonController fpsCont;
	public UnityEngine.UI.Text DebugLog;
	public GameObject GUI;


	string actType = "";

	public void Start()
	{
		//string initScreenName = "michaelhazani";
		createSphereFromNodes = GameObject.Find("SphereCenter").GetComponent<CreateSphereFromNodes>();

		inputField.Select();
		Cursor.lockState = CursorLockMode.None;
	}

	//	public void enter
	public void UIHelper(string actionType)
	{
		actType = actionType;
		print("action type is " + actType);
//		Cursor.visible = false;

		Cursor.lockState = CursorLockMode.Locked;
		var a = inputField.text;
		print("it is " + a);
		if (a != "")
		{ 
			inputField.text = "";
			GUI.SetActive(false);
//			Destroy(inputField.gameObject);
//			Destroy(getFollowersBtn.gameObject);
//			Destroy(getFriendsBtn.gameObject);
//			Destroy(getFollowingBtn.gameObject);
			controller.GetComponent<FirstPersonController>().enabled = true;
			//debugging and testing
			if (a == "1")
			{
				Init("1", new Vector3(0, 0, 0));
			}
			else if (a == "2")
			{
				Init("2", new Vector3(0, 0, 0));
			}
			else if (a == "3")
			{
				Init("3", new Vector3(0, 0, 0));
			}
			else if (a == "4")
			{
				Init("4", new Vector3(0, 0, 0));
			}
			else
			{ 
				Init(a, new Vector3(0, 0, 0));
			}
		}
	}

	public void Init(string screenName, Vector3 targetVector)
	{

		Vector3 a = targetVector;
		//Debug.Log(a);
		//Debug.Log("init fired");
		getFollowers(screenName, a);
        
	}

	void getFollowers(string screenName, Vector3 targetVector)
	{
		Vector3 a = targetVector;
		StartCoroutine(GetText(screenName, a));
	}

	IEnumerator GetText(string screenName, Vector3 targetVector)
	{
		print("screename is " + screenName);
		if (screenName != "PlayAreaScripts")
		{ 
			string request_String = "";

			if (actType == "followers")
			{
				print("getting followers");
				request_String = "http://localhost:3000/followers/" + screenName;
			}
			else if (actType == "friends")
			{
				print("getting friends");
				request_String = "http://localhost:3000/friends/" + screenName;
			}
			using (UnityWebRequest request = UnityWebRequest.Get(request_String))
			{
				yield return request.Send();

				if (request.isError) // Error
				{
					print(request.error);

				}
				else // Success
				{
					string response = request.downloadHandler.text;

					loadedData = JsonHelper.getJsonArray<SingleFollower>(response);

					createSphereFromNodes.CreateSphere(loadedData, screenName, targetVector);
				}
			}
		}
		else
		{
			print("hit Raycast hit PlayAreaScripts!");
		}
	}
}