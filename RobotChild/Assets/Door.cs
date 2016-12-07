using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	PlayerAbilities playerAbil;

	void Start() {
		playerAbil = GameObject.Find("robotChild").GetComponent<PlayerAbilities>();
	}

	void Update() {
		if (playerAbil.keyCount == 3) {
			Destroy(gameObject);
		}
	}

}
