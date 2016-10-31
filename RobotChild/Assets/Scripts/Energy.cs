using UnityEngine;
using System.Collections;

public class Energy : MonoBehaviour {

	public int energyMax;
	float eTimer;
	public float eTotTime;

	bool isDead;

	public TextMesh energy;

	powerCore pC;

	public void setPowerCore(powerCore core) {
		pC = core;
	}

	void Start () {

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
		}

		if (Input.GetKeyDown(KeyCode.E) & pC != null) {
			energyMax = 100;
			Destroy(pC.gameObject);
		}
	}

	void Die() {
		isDead = true;
		print("DEAD");
	}

	public bool getIsDead() {
		return isDead;
	}
}
