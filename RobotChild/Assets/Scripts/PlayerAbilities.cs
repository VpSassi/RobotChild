using UnityEngine;
using System.Collections;

public class PlayerAbilities : MonoBehaviour {

	public bool playDead;

	float pDedTimer;
	public float pDedTimerMax;

	Energy nrg;
	CharacterMovement cM;

	void Start() {
		nrg = GetComponent<Energy>();
		cM = GetComponent<CharacterMovement>();
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) && !nrg.getIsDead()) {
			playDead = !playDead;
		}		

		if (playDead == true) {
			pDedTimer += Time.deltaTime;
		}
		else {
			pDedTimer = 0;
		}
		if (pDedTimer > pDedTimerMax) {
			nrg.Die();
		}
	}
	public bool getPlayDead() {
		return playDead;
	}
}
