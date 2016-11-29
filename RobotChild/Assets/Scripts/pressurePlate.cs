using UnityEngine;
using System.Collections;

public class pressurePlate : MonoBehaviour {

	public Animator pPAnim;
	public Animator doorAnim;
	public bool isPressed;

	void Update() {

	if (isPressed == true){
			pPAnim.SetBool("isPressed", true);
			doorAnim.SetBool("Open", true);
			//print("click");
		}
	}

	void OnTriggerEnter(Collider plate) {

		if (plate.tag == ("Player")) {
			isPressed = true;
		}
		else {
			isPressed = false;
			pPAnim.SetBool("isPressed", false);
			doorAnim.SetBool("Open", false);
		}
	}



}
