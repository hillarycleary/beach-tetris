using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour {
	
	private Vector3 screenPoint;
	private Vector3 offset;
	private Animator animator;
	private bool thrown; 
	private bool land;

	void Awake(){
		animator = GetComponent<Animator> ();
		thrown = false;
	}
	
	void OnMouseDown(){
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		animator.SetInteger ("AnimState", 1);
	}
	
	void OnMouseDrag(){
		Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
		transform.position = cursorPosition;
		if (animator.GetCurrentAnimatorStateInfo (0).normalizedTime > 1 && !animator.IsInTransition (0)) {
			animator.SetInteger ("AnimState", 2);
		}
	}
	
	void OnMouseUpAsButton(){
		gameObject.transform.position = transform.position + Camera.main.transform.forward;
		Rigidbody rb = gameObject.GetComponent<Rigidbody>();
		rb.velocity = Camera.main.transform.forward*15+Camera.main.transform.up*20;
		animator.SetInteger ("AnimState", 3);

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
			if(col.gameObject.name == "SandCritter_1"||col.gameObject.name == "SandCritter_2"||col.gameObject.name == "Pile")
			{
				land=true;
			}
		}
			
	
}