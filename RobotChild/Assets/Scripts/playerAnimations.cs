using UnityEngine;
using System.Collections;

public class playerAnimations : MonoBehaviour {

	CharacterMovement charMov;
	PlayerAbilities playAb;
	Energy energy;
	powerCore powerCore;

	bool animTimer;
	float animTime;

	Animator pAnim;

	void Start () {

		pAnim = GetComponent<Animator>();
		charMov = GetComponent<CharacterMovement>();
		energy = GetComponent<Energy>();
		powerCore = GetComponent<powerCore>();
	}
	
	void Update () {
	
		if (charMov.moving == true && !energy.getIsDead()) {
			pAnim.Play("walkingAnimation");
		}
		else {
			pAnim.Play("Idle");
		}
	}
}
