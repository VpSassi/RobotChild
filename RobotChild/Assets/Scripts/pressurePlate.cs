using UnityEngine;
using System.Collections;

public class pressurePlate : MonoBehaviour {

	public Animator pPAnim;
	public Animator doorAnim;

	void OnTriggerEnter(Collider plate) {

		if (plate.tag == ("Player")) {
			pPAnim.SetBool("isPressed", true);
			doorAnim.SetBool("Open", true);
		}
	}



}
