using UnityEngine;
using System.Collections;

public class Moon : MonoBehaviour {

	private Animator animator;
	private bool hit;
	private bool hit_cont;
	private int LastFrame;

	float timeCounter=0;
	public float speed;
	public float width;
	public float height;


	// Use this for initialization
	void Awake () {
		animator = GetComponent<Animator> ();
		hit = false;
	}
	
	// Update is called once per frame
	void Update () {
		//movement
		timeCounter += Time.deltaTime * speed/100;
		float x = Mathf.Cos (timeCounter)* width;
		float y = Mathf.Sin (timeCounter)* height;
		float z = transform.position.z;

		Rigidbody rb = gameObject.GetComponent<Rigidbody> ();

		rb.transform.position= new Vector3(x,y,z);



		//animation
		if (animator.GetInteger ("AnimState") == 1) {
			if (animator.GetCurrentAnimatorStateInfo (0).normalizedTime > 1 && !animator.IsInTransition (0)) {
				hit = false;
			}
		}

		if (hit == true) {
			animator.SetInteger ("AnimState", 1);

		}


			
		if (hit == false){
				animator.SetInteger ("AnimState", 0);
			}
		}
			


	void OnCollisionEnter (Collision col)
	{
		
		if (col.gameObject.tag == "SC1" || col.gameObject.tag == "SC2" || col.gameObject.tag == "SC3" || col.gameObject.tag == "Crabtag") {
			hit = true;
			print ("hit");
			LastFrame = Time.frameCount;

		}
	}
}
