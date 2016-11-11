using UnityEngine;
using System.Collections;

public class AntinAnimaatiokontrolleri : MonoBehaviour {

    static Animator anim;
    public float speed = 5.0F;
    public float rotationSpeed = 100.0F;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);

        // Jump komento on spacebar 
        if (Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("battery low animation");
        }

        if(translation != 0)
        {
            anim.SetBool("Running", true);
        }

        else
        {
            anim.SetBool("Running", false);

        }
	}
}
