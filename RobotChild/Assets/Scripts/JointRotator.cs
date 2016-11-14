using UnityEngine;
using System.Collections;

public enum AxisToRotateAround { X, Y, Z };

public class JointRotator : MonoBehaviour {
	
	public AxisToRotateAround axisToRotateAround;
	Quaternion initialLocalRotation;

	public bool reverseDirection;

	public float maxRotation = 20.0f;

	void Start () {
		initialLocalRotation = transform.localRotation;
	}
	
	void Update () {
		Vector3 axis;
		if (axisToRotateAround == AxisToRotateAround.X) {
			axis = Vector3.right;
		} else if (axisToRotateAround == AxisToRotateAround.Y) {
			axis = Vector3.up;
		} else {
			axis = Vector3.forward;
		}

		Quaternion r = Quaternion.AngleAxis(maxRotation * Mathf.Sin (Time.time) * (reverseDirection ? -1 : 1), axis);
		transform.localRotation = initialLocalRotation * r;
	}
}
