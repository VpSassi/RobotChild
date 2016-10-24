using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	public float moveSpeed;


	void Start () {
	
	}
	

	void Update () {
		transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * moveSpeed * Time.deltaTime);

	}
}
