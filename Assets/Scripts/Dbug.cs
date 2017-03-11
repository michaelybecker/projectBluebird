using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Dbug : MonoBehaviour
{
	public UnityEngine.UI.Text DebugLog;
	public DbugMessage debugObject;
	public bool isDebugging;
	public float waitTime = 4.0f;
	// Use this for initialization
	void Start()
	{
		if (isDebugging)
		{
			DebugLog.enabled = true;
			getMessage();

		}


	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown("="))
		{
			isDebugging = !isDebugging;
			DebugLog.enabled = !DebugLog.isActiveAndEnabled;
		}
	}

	void getMessage()
	{
		InvokeRepeating("GMRelay", 0.0f, waitTime);
	}

	void GMRelay()
	{
		StartCoroutine(getMsg());
	}


	IEnumerator getMsg()
	{
		bool didOnce = false;
		string request_String = "http://localhost:3000/debug/";
		using (UnityWebRequest request = UnityWebRequest.Get(request_String))
		{
			yield return request.Send();

			if (request.isError) // Error
			{
				print(request.error);
				StopAllCoroutines();

			}
			else // Success
			{
				string response = request.downloadHandler.text;

				debugObject = JsonUtility.FromJson <DbugMessage>(response);

				print(debugObject.time + " " + debugObject.message);			
				DebugLog.text = debugObject.time + "\n" + debugObject.message;
				StopAllCoroutines();

			}


		}
	}
}
