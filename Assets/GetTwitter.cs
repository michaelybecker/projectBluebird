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
        Debug.Log(screenName);
        if (screenName != "PlayAreaScripts") { 
        string request_String = "http://localhost:3000/followers/" + screenName;
        //string request_String = "http://localhost:3000/dummy/100";
        Vector3 a = targetVector;
        //Debug.Log("gettwitter " + a);
        using (UnityWebRequest request = UnityWebRequest.Get(request_String))
        {
            yield return request.Send();

            if (request.isError) // Error
            {
                Debug.Log(request.error);
            }
            else // Success
            {
                string[] response = request.downloadHandler.text.Split(',');
                Debug.Log("get twitter script tvector: " + a); // already messed up
                createSphereFromNodes.CreateSphere(response, screenName, a);
                for (int i = 0; i < response.Length; i++)
                {
                    // Debug.Log(response[i]);
                }
            }
        }
     }
    }
}