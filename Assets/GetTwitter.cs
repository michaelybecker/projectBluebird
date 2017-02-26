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
        string initScreenName = "25";
        Init(initScreenName);
    }

    public void Init(string screenName)
    {
        Debug.Log("hello world!");
        createSphereFromNodes = GameObject.Find("SphereCenter").GetComponent<CreateSphereFromNodes>();
        getFollowers(screenName);
    }
 
    void getFollowers(string screenName)
    {
        StartCoroutine(GetText(screenName));
    }

    IEnumerator GetText(string screenName)
    {
        string request_String = "http://localhost:3000/dummyfollowers/" + screenName;
        using (UnityWebRequest request = UnityWebRequest.Get(request_String))
        {
            yield return request.Send();

            if (request.isError) // Error
            {
                Debug.Log(request.error);
            }
            else // Success
            {
                Debug.Log(request.downloadHandler.text);
                string[] response = request.downloadHandler.text.Split(',');
                Debug.Log("response is " + response.Length + "things long! creating things");
                createSphereFromNodes.CreateSphere(response);
                for (int i = 0; i < response.Length; i++)
                {
                   // Debug.Log(response[i]);
                }
            }
        }
    }
}