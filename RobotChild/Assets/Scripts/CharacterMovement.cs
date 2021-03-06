﻿using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	public float moveSpeed;

	public GameObject origCameraPos;
	public GameObject mainCam;
	public float offWall;
	public float smoothTime;

	public bool moving;

	Vector3 lastDir;

	PlayerAbilities pA;
	Energy enrg;
	TheEnd ending;

	public Animator pAnim;

	void Start() {
		pA = GetComponent<PlayerAbilities>();
		enrg = GetComponent<Energy>();
		//ending = GameObject.Find("THE END").GetComponent<TheEnd>();
	}

	void Update() {

		RaycastHit hit;
		var dir = origCameraPos.transform.position - transform.position;
		Debug.DrawRay(transform.position, dir);
		if (Physics.Raycast(transform.position, dir, out hit, dir.magnitude)) {
			mainCam.transform.position = hit.point - (dir.normalized * offWall);
		}
		else {
			mainCam.transform.position = origCameraPos.transform.position;
		}
			mainCam.transform.rotation = origCameraPos.transform.rotation;

		if (enrg.getIsDead()) {
			return;
		}


		if (!pA.getPlayDead() && !pA.getDancing() && !enrg.getEnergyAnimBool() && !pA.getpickUpAnim()) {

			float Horizontal = Input.GetAxis("Horizontal");
			float Vertical = Input.GetAxis("Vertical");
			Vector3 direction = Camera.main.transform.right * Horizontal + Camera.main.transform.forward * Vertical;
			//direction = Vector3.ProjectOnPlane(direction, Vector3.up);

			direction.y = 0;

			if (direction.magnitude > 1) {
				direction = direction.normalized;
				//direction.Normalize();
			}
			if (direction.magnitude > 0) {
				lastDir = direction;
			}

			Debug.DrawLine(transform.position, transform.position + lastDir * 5, Color.red);

			transform.position += direction * moveSpeed * Time.deltaTime;
			var nextRotation = Quaternion.identity;

			if (lastDir.magnitude > 0) {
				nextRotation = Quaternion.LookRotation(lastDir.normalized, Vector3.up);
			}

			if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) {
				transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, smoothTime);
				moving = true;
			}
			else {
				moving = false;
			}

		}

		if (moving == true) {
			pAnim.SetBool("Running", true);

		}
		else {
			pAnim.SetBool("Running", false);
		}



	}
}
