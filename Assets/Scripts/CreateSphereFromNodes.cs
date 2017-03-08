
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateSphereFromNodes : MonoBehaviour
{
	public GameObject LightSphere;
	public GameObject SphereLitFromWithin;
	List<GameObject> uspheres;
	public float turnSpeed;
	Camera cam;

	void Start()
	{
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		uspheres = new List<GameObject>();

	}
		
	//    public void CreateSphere(string[] a, string screenName, Vector3 targetVector)
	public void CreateSphere(SingleFollower[] a, string screenName, Vector3 targetVector)
	{
		int numPoints = a.Length;
		//Vector3 centerPoint;
		Vector3[] pts = PointsOnSphere(numPoints);
		float scaling = numPoints / 2;


		//foreach (Vector3 value in pts)
		//        Debug.Log(targetVector); //already messed up

		for (int j = 0; j < pts.Length; j++)
		{

			Vector3 value = pts[j];

			var tweetParentComponent = new GameObject();
			var ab = Instantiate(SphereLitFromWithin);
			uspheres.Add(tweetParentComponent);
			ab.transform.parent = tweetParentComponent.transform;
			ab.transform.localPosition = new Vector3(0, 0, 0);

			uspheres[j].tag = "tweetsphere";
			uspheres[j].name = a[j].screen_name;
			GameObject myTextObject = new GameObject(a[j].screen_name);
			myTextObject.AddComponent<TextMesh>();
			TextMesh textMeshComponent = myTextObject.GetComponent(typeof(TextMesh)) as TextMesh;
			textMeshComponent.text = a[j].screen_name;
			textMeshComponent.fontSize = 30;
			textMeshComponent.anchor = TextAnchor.MiddleCenter;
			textMeshComponent.transform.localScale = new Vector3((float)0.1, (float)0.1, (float)0.1); 
			myTextObject.tag = "tweetsphere";
			ab.transform.localPosition = new Vector3(0, 0, 0);
			//myTextObject.AddComponent<MeshRenderer>();
			MeshRenderer meshRendererComponent = myTextObject.GetComponent(typeof(MeshRenderer)) as MeshRenderer;


			myTextObject.transform.SetParent(tweetParentComponent.transform);
			Vector3 newVector = myTextObject.transform.position + Vector3.down;
			myTextObject.transform.position = newVector;

//			tweetParentComponent.transform.position = (value) * scaling + targetVector;
			tweetParentComponent.transform.position = (value) * scaling + Vector3.left * numPoints / 2;

			Vector3 heading = cam.transform.position - myTextObject.transform.position;
			myTextObject.transform.LookAt(myTextObject.transform.position - heading);

		}
	}


	Vector3[] PointsOnSphere(int n)
	{
		List<Vector3> upts = new List<Vector3>();
		float inc = Mathf.PI * (3 - Mathf.Sqrt(5));
		float off = 2.0f / n;
		float x = 0;
		float y = 0;
		float z = 0;
		float r = 0;
		float phi = 0;

		for (var k = 0; k < n; k++)
		{
			y = k * off - 1 + (off / 2);
			r = Mathf.Sqrt(1 - y * y);
			phi = k * inc;
			x = Mathf.Cos(phi) * r + (float)1;
			z = Mathf.Sin(phi) * r;

			upts.Add(new Vector3(x, y, z));
		}
		Vector3[] pts = upts.ToArray();
		return pts;
	}

	void Update()
	{
		if (uspheres.Count > 0)
		{
			foreach (GameObject obj in uspheres)
			{
				
				obj.transform.GetChild(0).transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
			}
		}
    

	}

}
