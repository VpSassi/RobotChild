using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

	private const float Y_ANGLE_MIN = 0;
	private const float Y_ANGLE_MAX = 50;

	public Transform lookAt;
	private Transform camTransform;

	private Camera cam;

	public float distance = 5;
	private float currentX = 0;
	private float currentY = 0;
	public float sensitivityX = 4;
	public float sensitivityY = 1; 

	void Start() {

		camTransform = transform;
		cam = Camera.main;
	}
	void Update() {
		currentX += Input.GetAxis("Mouse X");
		currentY += Input.GetAxis("Mouse Y");

		currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
		Vector3 dir = new Vector3(0, 0, -distance);
		Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
		camTransform.position = lookAt.position + rotation * dir;
		camTransform.LookAt(lookAt.position);
		cam.transform.rotation = camTransform.rotation;
	}
}
