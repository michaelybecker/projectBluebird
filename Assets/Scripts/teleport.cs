using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour {

    private float startTime;
    private float journeyLength;
    public float time = 3.0f;
    public float speed = 5.0f;
    public GameObject player;

 //   // Use this for initialization
 //   void Start () {
		
	//}
	
	//// Update is called once per frame
	//void Update () {
		
	//}
    
    public void startGTP(Vector3 dest)
    {
        StartCoroutine(GoToPlanet(dest));
    }

    private IEnumerator GoToPlanet(Vector3 dest) {
        print("going to " + dest + " in " + time);
        Vector3 curPos = player.transform.position;
        float elapsed = 0;
        while (elapsed < (time))
        {

            player.transform.position = Vector3.Lerp(player.transform.position, dest, (elapsed / time * speed));
            yield return null;

            //print("curpos: " + curPos);
            //print("dest: " + dest);
            elapsed += Time.deltaTime;
        }
        print("Done!");
        player.transform.position = dest;
        elapsed = 0.0f;

    }
}
