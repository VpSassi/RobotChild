using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AcquireTarget : MonoBehaviour {

    public GameObject[] robotChildren;





    void Start() {
        robotChildren = GameObject.FindGameObjectsWithTag("RobotChild");
        print(robotChildren);
    }





	//public GameObject ClosestTarget() {

 //   }
}
