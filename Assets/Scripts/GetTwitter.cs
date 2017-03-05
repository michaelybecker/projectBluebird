using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


public class GetTwitter : MonoBehaviour
{

	//ArrayList FollowersList = new ArrayList();
	public CreateSphereFromNodes createSphereFromNodes;


	public void Start()
	{
		// Debug.Log("hello world!");
		// createSphereFromNodes = GameObject.Find("SphereCenter").GetComponent<CreateSphereFromNodes>();
		// getFollowers();
		//
		string initScreenName = "michaelhazani";
		Vector3 initVector = new Vector3(0, 0, 0);
		Init(initScreenName, initVector);
	}

	public void Init(string screenName, Vector3 targetVector)
	{

		Vector3 a = targetVector;
		Debug.Log(a);
		Debug.Log("init fired");
		createSphereFromNodes = GameObject.Find("SphereCenter").GetComponent<CreateSphereFromNodes>();
		getFollowers(screenName, a);
	}

	void getFollowers(string screenName, Vector3 targetVector)
	{
		Vector3 a = targetVector;
		StartCoroutine(GetText(screenName, a));
	}

	IEnumerator GetText(string screenName, Vector3 targetVector)
	{
		Debug.Log("screename is " + screenName);
		if (screenName != "PlayAreaScripts")
		{ 
			string request_String = "http://localhost:3000/followers/" + screenName;
			Vector3 a = targetVector;
			using (UnityWebRequest request = UnityWebRequest.Get(request_String))
			{
				yield return request.Send();

				if (request.isError) // Error
				{
					Debug.Log(request.error);
				}
				else // Success
				{
					string response = request.downloadHandler.text;
					SingleFollower[] loadedData = JsonHelper.getJsonArray<SingleFollower>(response);
					foreach (var re in loadedData)
					{ 
						Debug.Log(re.screen_name);
					}
					createSphereFromNodes.CreateSphere(loadedData, screenName, targetVector);
				}
			}
		}
		else
		{
			Debug.Log("hit Raycast hit PlayAreaScripts!");
		}
	}
}