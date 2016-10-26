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
		float Horizontal = Input.GetAxis("Horizontal");
		float Vertical = Input.GetAxis("Vertical");
		Vector3 direction = Camera.main.transform.right * Horizontal + Camera.main.transform.forward * Vertical;
		direction = Vector3.ProjectOnPlane(direction, Vector3.up);

		transform.position += direction.normalized * moveSpeed * Time.deltaTime;

		var v = Camera.main.transform.forward;
		v.y = 0.0f;
		v.Normalize();

		transform.rotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
		
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
