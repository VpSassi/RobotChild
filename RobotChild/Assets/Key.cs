using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

	PlayerAbilities playAb;

	void Start() {
		playAb = GameObject.Find("robotChild").GetComponent<PlayerAbilities>();
	}

	void OnTriggerEnter(Collider other) {

		if (other.tag == ("Player")) {
			playAb.setKey(this);
		}
	}
	void OnTriggerExit(Collider other) {
		if (other.tag == ("Player")) {
			playAb.setKey(null);
		}
	}
}
