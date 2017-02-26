using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// you need this: https://github.com/SaladLab/Json.Net.Unity3D/releases  (don't install lite version, get full!)  
using Newtonsoft.Json.Linq;

public class parseJSON{
    public static List<UserModel.TwitterUser> returnNamesFromJSON(string json)
    {

        JArray a = JArray.Parse(json);
        var outList = new List<UserModel.TwitterUser>();

        foreach (JObject o in a.Children<JObject>())
        {
            string screenName = "";
            string profilePic = "";

            foreach (JProperty p in o.Properties())
            {
                if (p.Name == "screen_name")
                 {
                    screenName = (string)p.Value;
                 }
                if (p.Name == "profile_image_url")
                {
                    profilePic = (string)p.Value;
                }

            }
            outList.Add(new UserModel.TwitterUser
            {
                screenName = screenName,
                profileImageUrl = profilePic
            });
        }
        return outList;
    }
    // Use this for initialization
    void Start () {
        //Debug.Log("Hello World!");
        //string json = @"
        //    [ 
        //        { ""General"" : ""At this time we do not have any frequent support requests."",
        //        ""screen_name"" : ""scoobert doobert "" },
        //        { ""Support"" : ""For support inquires, please see our support page."",
        //        ""screen_name"" : ""sceebert deebert"" }
        //    ]";

        //var newList = returnNamesFromJSON(json);

        //foreach (UserModel.TwitterUser User in newList)
        //{
        //  Debug.Log(User.profileImageUrl);
        //    Debug.Log(User.screenName);
        //}
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
