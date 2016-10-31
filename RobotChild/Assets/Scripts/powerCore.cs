using UnityEngine;
using System.Collections;

public class powerCore : MonoBehaviour {

	Energy e;

	void Start() {
		e = GameObject.Find("robotChild").GetComponent<Energy>();
	}

	void OnTriggerEnter(Collider other) {

		if (other.tag == ("Player")) {
			e.setPowerCore(this);
		}
	}
	void OnTriggerExit(Collider other){
		if (other.tag == ("Player")) {
			e.setPowerCore(null);
		}
	}
}
