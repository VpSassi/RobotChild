using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TheEnd : MonoBehaviour {

	public GameObject roboBoi;

	public bool end;

	float endTimer;
	public float endTotTimer;

	void Start () {
		roboBoi = GameObject.Find("robotChild");		
	}
	

	void OnTriggerEnter(Collider col) {
		if (col.tag == ("Player")) {
			endTimer += Time.deltaTime;
			if(endTimer > endTotTimer) {
			end = true;
			SceneManager.LoadScene(0);
			}
		}
	}
	public bool getEnd() {
		return end;
	}
}
