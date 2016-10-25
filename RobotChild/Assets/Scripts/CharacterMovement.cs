using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	public float moveSpeed;

	public GameObject origCameraPos;
	public GameObject mainCam;
	public float offWall;

	void Start () {

	}
	

	void Update () {
		transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * moveSpeed * Time.deltaTime);
		var v = Camera.main.transform.forward;
		v.y = 0.0f;
		v.Normalize();
		transform.forward = v;
		
		RaycastHit hit;
		var dir = origCameraPos.transform.position - transform.position;
		Debug.DrawRay(transform.position, dir);
		if (Physics.Raycast(transform.position, dir, out hit, dir.magnitude)) {
			mainCam.transform.position = hit.point - (dir.normalized * offWall);
		}
		else {
			mainCam.transform.position = origCameraPos.transform.position;
		} 
	}
}
