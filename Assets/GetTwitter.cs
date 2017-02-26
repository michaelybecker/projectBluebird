using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GetTwitter : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        using (UnityWebRequest request = UnityWebRequest.Get("http://localhost:3000/"))
        {
            yield return request.Send();

            if (request.isError) // Error
            {
                Debug.Log(request.error);
            }
            else // Success
            {
                Debug.Log(request.downloadHandler.text);
            }
        }
    }
}