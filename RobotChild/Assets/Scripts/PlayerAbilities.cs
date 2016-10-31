using UnityEngine;
using System.Collections;

public class PlayerAbilities : MonoBehaviour {

	public bool playDead;

	Energy nrg;

	void Start() {
		nrg = GetComponent<Energy>();
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) && !nrg.getIsDead()) {
			playDead = !playDead;
		}
	}
	public bool getPlayDead() {
		return playDead;
	}
}
