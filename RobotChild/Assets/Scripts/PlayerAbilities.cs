using UnityEngine;
using System.Collections;

public class PlayerAbilities : MonoBehaviour {

	public bool playDead;
	public bool pickup;
	public bool dance;

	public bool pickUpAnimBool;
	float pickUpAnimTimer;
	public float pickUpAnimTotTimer;

	bool standingUpBool;
	float standUpTime;
	public float standUpTotTime;

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

        if (playDead) {
            lightValue = 0;
        }
        else {
            lightValue = Mathf.Clamp(lightValue, lightMin, lightMax);
        }

		debugSphere.localScale = new Vector3(lightValue * 2, lightValue * 2, lightValue * 2);
		Debug.DrawRay(transform.position, (enemy.transform.position - transform.position).normalized * lightValue);

		if (Vector3.Distance(transform.position, enemy.transform.position) < lightValue) {
			//print("ALERT");
		}
		else {
			//print("safe");
		}

		if (Input.GetButtonDown("playDead") && !nrg.getIsDead()) {
			playDead = !playDead;
            lightValue = lightMin;
		}
		/*
		if (Input.GetButtonDown("playDead") && !nrg.getIsDead() && playDead == true) {
			standUpTime += Time.deltaTime;
			standingUpBool = true;
		}

		if (standUpTime > standUpTotTime) {
			standUpTime -= standUpTotTime;
			standingUpBool = false;
			print("bing");
		} */
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

		if (pickUpAnimBool == true) {
			pickUpAnimTimer += Time.deltaTime;
		}

		if (pickUpAnimTimer > pickUpAnimTotTimer) {
			pickUpAnimTimer -= pickUpAnimTotTimer;
			pickUpAnimBool = false;
		}

		if (Input.GetButton("pickUp")) {
			core = nrg.getPowerCore();
			if (core != null) {
				Destroy(core.gameObject);
				print ("picked up a power core");
				pickup = true;
                Fabric.EventManager.Instance.PostEvent("PickUp");
				pAnim.SetBool("pickUp", true);
			    pickUpAnimBool = true;
            }
			else {
				pickup = false;
				pAnim.SetBool("pickUp", false);
			}

		if (pickup == true && (Input.GetKeyDown(KeyCode.F))) {
				print("dropped");
			}
		}

		if (Input.GetKeyDown(KeyCode.V) && cM.moving == false && !nrg.getIsDead()) {
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

	public bool getStandingUp() {
		return standingUpBool;
	}

	public bool getpickUpAnim() {
		return pickUpAnimBool;
	}
}
