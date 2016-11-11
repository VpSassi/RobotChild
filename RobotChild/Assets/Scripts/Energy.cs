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
			energy.text = "DEAD";
			//energy.font.material.color = Color.red;
			return;
		}

		if (Input.GetKeyDown(KeyCode.M) && !getIsDead()) {
			energyMax = 3;
		}
		if (Input.GetKeyDown(KeyCode.N) && !getIsDead()) {
			energyMax = 23;
		}
		if (Input.GetKeyDown(KeyCode.T) && !getIsDead()) {
			energyMax = 100;
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

		if (energyMax <= 20 && charMov.moving == false && !getIsDead()) {
			pAnim.SetBool("drunk idle", true);
		}
		else {
			pAnim.SetBool("drunk idle", false);
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
			pAnim.SetBool("sprint", true);
		}
		else if (energyMax > 20){
			charMov.moveSpeed = standardSpeed;
			pAnim.SetBool("sprint", false);
		} 
	}

	public void Die() {
		isDead = true;
		print("DEAD");
		pAnim.SetBool("Dead", true);
	}

	public bool getIsDead() {
		return isDead;
	}
}
