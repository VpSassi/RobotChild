using UnityEngine;
using System.Collections;

public class Energy : MonoBehaviour {

	public int energyMax;
	float eTimer;
	public float eTotTime;

	bool isDead;

	public TextMesh energy;

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
	}

	void Die() {
		isDead = true;
		print("DEAD");
	}
}
