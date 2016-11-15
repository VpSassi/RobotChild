using UnityEngine;
using System.Collections;

public class PlayerAbilities : MonoBehaviour {

	public bool playDead;
	public bool pickup;
	public bool dance;

	float pDedTimer;
	public float pDedTimerMax;

	public GameObject enemy;

	public Transform debugSphere;

	Energy nrg;
	CharacterMovement cM;
	powerCore core;

	public float lightMax;
	public float lightMin;
	public float lightValue;
	public float lightAdd;

	public Animator pAnim;

	void Start() {
		nrg = GetComponent<Energy>();
		cM = GetComponent<CharacterMovement>();
		
	}
	
	void Update () {

		lightValue += Input.GetAxis("Mouse ScrollWheel") * lightAdd;

		lightValue = Mathf.Clamp(lightValue, lightMin, lightMax);

		debugSphere.localScale = new Vector3(lightValue * 2, lightValue * 2, lightValue * 2);
		Debug.DrawRay(transform.position, (enemy.transform.position - transform.position).normalized * lightValue);

		if (Vector3.Distance(transform.position, enemy.transform.position) < lightValue) {
			//print("ALERT");
		}
		else {
			//print("safe");
		}

		if (Input.GetKeyDown(KeyCode.Space) && !nrg.getIsDead()) {
			playDead = !playDead;
            lightValue = lightMin;
		}
	

		if (playDead == true) {
			pDedTimer += Time.deltaTime;
			pAnim.SetBool("playDead", true);
		}
		else {
			pDedTimer = 0;
			pAnim.SetBool("playDead", false);
		}
		if (pDedTimer > pDedTimerMax) {
			nrg.Die();
		}



		if (Input.GetKeyDown(KeyCode.F)) {
			core = nrg.getPowerCore();
			if (core != null) {
				print ("picked up a power core");
				pickup = true;
			}
			else {
				pickup = false;
			}

		if (pickup == true && (Input.GetKeyDown(KeyCode.F))) {
				print("dropped");
			}
		}

		if (Input.GetKeyDown(KeyCode.V) && !nrg.getIsDead()) {
			dance = !dance;
		}

		if (dance == true) {
			pAnim.SetBool("dancing",true);
		} else {
			pAnim.SetBool("dancing", false);
		}
	}
	public bool getDancing() {
		return dance;
	}

	public bool getPlayDead() {
		return playDead;
	}
}
