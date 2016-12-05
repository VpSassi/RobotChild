using UnityEngine;
using System.Collections;

public class otherChild : MonoBehaviour {

	public Animator otherChildAnim;

	public float normalSpeed;
	public float fastSpeed;

	public bool isFollowing;

	PlayerAbilities roboAb;
	CharacterMovement roboMov;
	Energy roboEnergy;
	public GameObject player;


	void Start () {
		roboAb = GameObject.Find("robotChild").GetComponent<PlayerAbilities>();
		roboMov = GameObject.Find("robotChild").GetComponent<CharacterMovement>();
		roboEnergy = GameObject.Find("robotChild").GetComponent<Energy>();
	}
	
	void Update () {
	
		if (isFollowing == true && !roboEnergy.isSprinting) {
			print("follow");
		}
		else if (isFollowing == true && roboEnergy.isSprinting) {
			print("followFaster");
		}
		if (isFollowing == true && roboAb.playDead) {
			print("playDead");
		}
	}

	void OnTriggerStay (Collider col) {

		if (col.tag == ("Player") && roboAb.dance) {
			isFollowing = true;
		}

	
	}
}
