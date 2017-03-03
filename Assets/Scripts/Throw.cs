using UnityEngine;
using System.Collections;



public class Throw : MonoBehaviour {

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

		
		print ("mouse position" + oldMouseY);
		Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
		transform.position = cursorPosition;
		if (animator.GetCurrentAnimatorStateInfo (0).normalizedTime > 1 && !animator.IsInTransition (0)) {
			animator.SetInteger ("AnimState", 2);
		}
	}

	void OnMouseUpAsButton(){
		mouseSpeedY = (oldMouseY - Input.mousePosition.y)/5;
		print ("mouse position" + Input.mousePosition.y);
		print ("mouse speed" + mouseSpeedY);
		//gameObject.transform.position = transform.position + Camera.main.transform.forward;
		Rigidbody rb = gameObject.GetComponent<Rigidbody>();
		rb.velocity = Camera.main.transform.forward * -mouseSpeedY + Camera.main.transform.up * -mouseSpeedY;
		//rb.AddForce(mouseSpeed * speed * -10, ForceMode.Force);

		if (rb.velocity.z > 1) {
			animator.SetInteger ("AnimState", 3);
		}

		thrown = true;
	}

	void Update(){
		if (thrown == true) {
			if (land && animator.GetInteger ("AnimState") < 4) {
				animator.SetInteger ("AnimState", Random.Range (4, 7));
			}
		}
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "SC2"||col.gameObject.tag == "SC1"||col.gameObject.name == "Pile")
		{
			if (gameObject.transform.position.z> 40) {
				land = true;
			}
		}
		if(col.gameObject.name == "Beach")
		{
			animator.SetInteger ("AnimState", 0);
		}
	}


}