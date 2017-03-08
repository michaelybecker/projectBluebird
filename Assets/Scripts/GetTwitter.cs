using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


public class GetTwitter : MonoBehaviour
{
	public SingleFollower[] loadedData;
	//ArrayList FollowersList = new ArrayList();
	public CreateSphereFromNodes createSphereFromNodes;


	public void Start()
	{
		string initScreenName = "michaelhazani";
		createSphereFromNodes = GameObject.Find("SphereCenter").GetComponent<CreateSphereFromNodes>();
		Init("michaelhazani", new Vector3(0, 0, 0));
	}

	public void Init(string screenName, Vector3 targetVector)
	{

		Vector3 a = targetVector;
		Debug.Log(a);
		Debug.Log("init fired");
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
//					print(response);
					loadedData = JsonHelper.getJsonArray<SingleFollower>(response);

//					foreach (var re in loadedData)
//					{ 
//						Debug.Log(re.profile_image_url);
//						print(re.id);
//						print(re.screen_name);
//						print(re.name);
//						print(re.location);
//						print(re.url);
//						print(re.description);
//						print(re.followers_count);
//						print(re.friends_count);
//						print(re.profile_image_url);
//						print("\n");
//					}
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