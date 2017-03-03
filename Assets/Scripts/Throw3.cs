using UnityEngine;
using System.Collections;



public class Throw3 : MonoBehaviour {

	private Vector3 screenPoint;
	private Vector3 offset;
	private Animator animator;
	private bool thrown; 
	private bool land;
	private float oldMouseY;
	private float oldMouseX;
	private float mouseSpeedX;
	private float mouseSpeedY;
	private int LastFrame;
	private float bodyspeed;
	private bool inPile;
	private bool stuck;
	public PhysicMaterial slippy;
	public PhysicMaterial landed;
	private float UpSpeed=100;
	private float ForwardSpeed=50;	


	void Awake(){
		animator = GetComponent<Animator> ();
		thrown = false;
		LastFrame = Time.frameCount;
	}

	void OnMouseDown(){
		
		if (Time.frameCount - LastFrame>20) {
			LastFrame = Time.frameCount;
			oldMouseY = Input.mousePosition.y;
			oldMouseX = Input.mousePosition.x;
		}
		Rigidbody rb = gameObject.GetComponent<Rigidbody>();
		bodyspeed = rb.velocity.y;

		thrown = false;
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		animator.SetInteger ("AnimState", 1);
	}

	void OnMouseDrag(){
		if (Time.frameCount - LastFrame>20) {
			LastFrame = Time.frameCount;
			oldMouseY = Input.mousePosition.y;
			oldMouseX = Input.mousePosition.x;
		}
		Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
		transform.position = cursorPosition;
		if (animator.GetCurrentAnimatorStateInfo (0).normalizedTime > 1 && !animator.IsInTransition (0)) {
			animator.SetInteger ("AnimState", 2);
		}
	}

	void OnMouseUpAsButton(){
		mouseSpeedY = (oldMouseY - Input.mousePosition.y)/5;
		Rigidbody rb = gameObject.GetComponent<Rigidbody>();
		rb.velocity = Camera.main.transform.forward * -mouseSpeedY*ForwardSpeed/100 + Camera.main.transform.up * -mouseSpeedY/3*UpSpeed/100;
		//rb.AddForce(mouseSpeed * speed * -10, ForceMode.Force);

		if (rb.velocity.z > 1) {
			animator.SetInteger ("AnimState", 3);
		}

		thrown = true;
	}

	void FixedUpdate(){
		if (thrown == true) {
			if (land && animator.GetInteger ("AnimState") < 4) {
				animator.SetInteger ("AnimState", Random.Range (4, 7));
			}
		}
		if (inPile) {
			Rigidbody rb = gameObject.GetComponent<Rigidbody> ();
			rb.MovePosition(new Vector3 (transform.position.x, transform.position.y, 51));
			GetComponent<BoxCollider>().material = landed;
		}
			



		}



	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.tag == "SC3" || col.gameObject.tag == "SC2" || col.gameObject.tag == "SC1" || col.gameObject.name == "Pile") {
			if (inPile) {
				land = true;
			} else {
				animator.SetInteger ("AnimState", 2);
			}
		
			if (inPile) {
				Rigidbody rb = gameObject.GetComponent<Rigidbody> ();
				GetComponent<BoxCollider>().material = landed;
				if (rb.velocity.y < .5 && rb.velocity.y > -.5 && rb.velocity.x < .5 && rb.velocity.x > -.5 && rb.angularVelocity.magnitude < 1 && stuck == false) {
					if (col.gameObject.name == "Pile") {							
						stuck = true;
					} else {
						stuck = false;

					}


				}


			}
		}




		if(col.gameObject.name == "Beach")
		{
			animator.SetInteger ("AnimState", 0);
		}
		if(col.gameObject.name == "Moon")
		{
			animator.SetInteger ("AnimState", 2);
		}
	}


	void OnTriggerEnter(Collider col){
		Rigidbody rb = gameObject.GetComponent<Rigidbody>();
		if(col.gameObject.name == "pileTrigger" && rb.velocity.y <= .1)
		{
			if (col.gameObject.tag == "SC3" || col.gameObject.tag == "SC2" || col.gameObject.tag == "SC1") {
				
			}else if(rb.velocity.y <= .1){
			print ("in pile");
			inPile = true;
		}
	}
}
}