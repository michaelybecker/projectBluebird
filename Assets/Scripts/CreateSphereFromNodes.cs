
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CreateSphereFromNodes : MonoBehaviour
{
	public GameObject LightSphere;
	public GameObject SphereLitFromWithin;
	List<GameObject> uspheres;
	public float turnSpeed;
	public Font Exo, Titillum;
	Camera cam;
	Color pink = new Color(243 / 255.0F, 41 / 255.0F, 190 / 255.0F, 1);

	void Start()
	{
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		uspheres = new List<GameObject>();
//		Exo = (Font)Resources.Load("Exo.otf");
//		Titillum = (Font)Resources.Load("Titillum.otf");
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

			int fNum = Convert.ToInt32(a[j].followers_count);
			uspheres.Add(tweetParentComponent);
			ab.transform.parent = tweetParentComponent.transform;
			ab.transform.localPosition = new Vector3(0, 0, 0);
			Collider col = ab.AddComponent<SphereCollider>();
			ab.tag = "tweetsphere";
			ab.name = a[j].screen_name;
			GameObject myTextObject = new GameObject(a[j].screen_name);
			myTextObject.AddComponent<TextMesh>();
			TextMesh textMeshComponent = myTextObject.GetComponent(typeof(TextMesh)) as TextMesh;
			textMeshComponent.font = Exo;
			textMeshComponent.text = a[j].screen_name;
			textMeshComponent.fontSize = 50;
			textMeshComponent.anchor = TextAnchor.MiddleCenter;
			textMeshComponent.transform.localScale = new Vector3((float)0.1, (float)0.1, (float)0.1); 
			MeshRenderer meshRendererComponent = myTextObject.GetComponentInChildren<MeshRenderer>();
			meshRendererComponent.material = textMeshComponent.font.material;
			myTextObject.tag = "tweetsphere";
			myTextObject.name = a[j].screen_name;
			ab.transform.localPosition = new Vector3(0, 0, 0);
			//myTextObject.AddComponent<MeshRenderer>();

			//per size of followers
			float sphereSizePct;
			Color scaleColor;
			if (fNum < 100)
			{
				sphereSizePct = 1.0f;

				scaleColor = pink;
			}
			else if (fNum < 500)
			{
				sphereSizePct = 1.5f;
				scaleColor = Color.red;
			}
			else if (fNum < 1000)
			{
				sphereSizePct = 3.0f;
				scaleColor = Color.yellow;
			}
			else if (fNum < 5000)
			{
				sphereSizePct = 10.0f;
				scaleColor = Color.white;
			}
			else
			{
				sphereSizePct = 15f;
				scaleColor = Color.blue;
			}

			ab.transform.localScale *= sphereSizePct;
			ab.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
			ab.GetComponent<Renderer>().material.SetColor("_EmissionColor", scaleColor);
			ab.GetComponent<Renderer>().material.color = scaleColor;


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
