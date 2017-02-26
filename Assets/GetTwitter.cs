using System.Collections;
using System.Collections.Generic;
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
        using (UnityWebRequest request = UnityWebRequest.Get("http://localhost:3000/followers/michaelhazani"))
        {
            yield return request.Send();

            if (request.isError) // Error
            {
                Debug.Log(request.error);
            }
            else // Success
            {
                var results = new List<UserModel.TwitterUser>();
                results = parseJSON.returnNamesFromJSON(request.downloadHandler.text);

                foreach (UserModel.TwitterUser user in results)
                {
                    Debug.Log(user.screenName);
                    Debug.Log(user.profileImageUrl);
                }
              //  Debug.Log(request.downloadHandler.text);
            }
        }
    }
}