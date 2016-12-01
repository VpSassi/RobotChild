using UnityEngine;
using System.Collections;
using UnityEngine.UI;
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
	public float maxDetectionDistance;

	public int keyCount;

	public GameObject enemy;

	public Transform debugSphere;

	Energy nrg;
	CharacterMovement cM;
	powerCore core;
	Key key;

	public float lightMax;
	public float lightMin;
	public float lightValue;
	public float lightAdd;

	public Animator pAnim;

	public Image fadeImage;

	public void setKey(Key bling) {
		key = bling;
	}

	public Key getThisKey() {
		return key;
	}

	void Start() {
		nrg = GetComponent<Energy>();
		cM = GetComponent<CharacterMovement>();
		
	}
	
	void Update () {

		if (Input.GetButton("addLight")) {
			lightValue += lightAdd * Time.deltaTime;
		}
		if (Input.GetButton("takeLight")) {
			lightValue -= lightAdd * Time.deltaTime;
		}

		//print(lightValue);

		lightValue += Input.GetAxis("Mouse ScrollWheel") * lightAdd;
		lightValue = Mathf.Clamp(lightValue, lightMin, lightMax);
		fadeImage.color = new Color(0, 0, 0, lightMax - lightValue);


		if (playDead) {
            lightValue = 0;
        }
 
		debugSphere.localScale = new Vector3(lightValue * maxDetectionDistance, lightValue * maxDetectionDistance, lightValue * maxDetectionDistance);
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
			key = getThisKey();
			//print(keyCount);
			if (key != null) {
				Destroy(key.gameObject);
				//print ("picked up a key");
				pickup = true;
                Fabric.EventManager.Instance.PostEvent("PickUp");
				pAnim.SetBool("pickUp", true);
			    pickUpAnimBool = true;
				keyCount += 1;
            }

			else {
				pickup = false;
				pAnim.SetBool("pickUp", false);
			}
		}

        if (Input.GetButtonDown("Dance") && cM.moving == false && !nrg.getIsDead()) {
			dance = !dance;
			print(dance);
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
