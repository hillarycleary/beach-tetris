using UnityEngine;
using System.Collections;

public class Camera_move : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public float speed;
	
	void Update()
	{
		if(Input.GetKey(KeyCode.D))
		{
			transform.Translate(new Vector3(speed * Time.deltaTime,0,0));
		}
		if(Input.GetKey(KeyCode.A))
		{
			transform.Translate(new Vector3(-speed * Time.deltaTime,0,0));
		}
	}
}
