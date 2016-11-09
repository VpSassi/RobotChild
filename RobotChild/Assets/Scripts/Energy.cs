using UnityEngine;
using System.Collections;

public class Energy : MonoBehaviour {

	public int energyMax;
	float eTimer;
	public float eTotTime;

	public float lowPowerSpeed;
	public float standardSpeed;
	public float sprintSpeed;

	bool isDead;

	public TextMesh energy;
	public ParticleSystem pCoreParticle;

	powerCore pC;
	CharacterMovement charMov;
	PlayerAbilities pAbi;

	public Animator pAnim;

	public void setPowerCore(powerCore core) {
		pC = core;
	}

	public powerCore getPowerCore() {
		return pC;
	}

	void Start () {
		charMov = GetComponent<CharacterMovement>();
		pAbi = GetComponent<PlayerAbilities>();
	}
	

	void Update () {

		energy.text = "" + energyMax;

		if (isDead) {

			return;
		}

		eTimer += Time.deltaTime;

		if (eTimer > eTotTime) {
			eTimer -= eTotTime;
			energyMax -= 1;
		}

		if (energyMax < 0) {
			Die();
			// Animator set death
		}

		if (energyMax <= 20 && charMov.moving == true && !getIsDead()) {
			pAnim.SetBool("lowPower", true);
			charMov.moveSpeed = lowPowerSpeed;
		}
		else {
			pAnim.SetBool("lowPower", false);
			charMov.moveSpeed = standardSpeed;
		}

		if (Input.GetKeyDown(KeyCode.E) & pC != null) {
			energyMax = 100;
			Destroy(pC.gameObject);
			Instantiate(pCoreParticle, pC.transform.position, Quaternion.identity);
		}

		if (Input.GetKey(KeyCode.LeftShift) && energyMax > 20 && !getIsDead()) {
			charMov.moveSpeed = sprintSpeed;
			//pAnim.SetBool("sprint", true);
		}
		else {
			charMov.moveSpeed = standardSpeed;
			//pAnim.SetBool("sprint", false);
		}
	}

	public void Die() {
		isDead = true;
		print("DEAD");
		//pAnim.SetBool("dead", true);
	}

	public bool getIsDead() {
		return isDead;
	}
}
