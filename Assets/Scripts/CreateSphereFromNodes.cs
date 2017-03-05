
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateSphereFromNodes : MonoBehaviour
{
	Camera cam;

	void Start()
	{
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}
		
	//    public void CreateSphere(string[] a, string screenName, Vector3 targetVector)
	public void CreateSphere(SingleFollower[] a, string screenName, Vector3 targetVector)
	{
		int numPoints = a.Length;
		//Vector3 centerPoint;
		Vector3[] pts = PointsOnSphere(numPoints);
		float scaling = numPoints / 2;
		List<GameObject> uspheres = new List<GameObject>();
		int i = 0;

		//foreach (Vector3 value in pts)
//        Debug.Log(targetVector); //already messed up
		for (int j = 0; j < pts.Length; j++)
		{

			Vector3 value = pts[j];
			uspheres.Add(GameObject.CreatePrimitive(PrimitiveType.Sphere));
			//GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			//uspheres.Add(sphere);
			//Light tempLight = sphere.gameObject.AddComponent<Light>();
            
			//tempLight.color = Color.yellow;
			uspheres[i].transform.parent = transform;
			uspheres[i].transform.position = (value) * scaling + targetVector;

            
			uspheres[i].tag = "tweetsphere";
			uspheres[i].name = a[j].screen_name;
			GameObject myTextObject = new GameObject(a[j].screen_name);
			myTextObject.AddComponent<TextMesh>();
			// Get components
			TextMesh textMeshComponent = myTextObject.GetComponent(typeof(TextMesh)) as TextMesh;
            
			textMeshComponent.text = a[j].screen_name;
			textMeshComponent.fontSize = 30;
			textMeshComponent.transform.localScale = new Vector3((float)0.1, (float)0.1, (float)0.1); 
			//myTextObject.AddComponent<MeshRenderer>();
			MeshRenderer meshRendererComponent = myTextObject.GetComponent(typeof(MeshRenderer)) as MeshRenderer;
			myTextObject.tag = "tweetsphere";
			myTextObject.transform.position = (value) * scaling + targetVector;
			//Debug.LogWarning(targetVector);
			float up = myTextObject.transform.position.y + (float)2.0;
			myTextObject.transform.position.Set(myTextObject.transform.position.x, up, myTextObject.transform.position.z);
			Vector3 heading = cam.transform.position - myTextObject.transform.position;
			myTextObject.transform.LookAt(myTextObject.transform.position - heading);
			myTextObject.transform.SetParent(uspheres[i].transform);
			i++;
            
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
}