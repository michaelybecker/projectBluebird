using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


public class GetTwitter : MonoBehaviour
{

    //ArrayList FollowersList = new ArrayList();
    public CreateSphereFromNodes createSphereFromNodes;


    void Start()
    {
        createSphereFromNodes = GameObject.Find("SphereCenter").GetComponent<CreateSphereFromNodes>();
        getFollowers();
    }


    void getFollowers()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        using (UnityWebRequest request = UnityWebRequest.Get("http://localhost:3000/dummy/256"))
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
                    Debug.Log(response[i]);
                }


            }
        }
    }
}