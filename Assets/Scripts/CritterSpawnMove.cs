using UnityEngine;
using System.Collections;

public class CritterSpawnMove : MonoBehaviour {


	float timeCounter=0;
	public float speed;
	public float width;

	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
		timeCounter += Time.deltaTime * speed/100;
		float x = Mathf.Cos (timeCounter)* width;
		gameObject.transform.position= new Vector3(x,transform.position.y,transform.position.z);
	
	}
		
}
