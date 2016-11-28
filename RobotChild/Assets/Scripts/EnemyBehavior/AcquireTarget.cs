using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class AcquireTarget : MonoBehaviour {

    GameObject[] robotChildren;
    float[] distancesToChildren;




    void Start() {
        robotChildren = GameObject.FindGameObjectsWithTag("robotChild");
        distancesToChildren = new float[robotChildren.Length];
    }

    void Update() {
        print(ClosestChild());
    }





    public GameObject ClosestChild() {
        for (int i = 0; i < distancesToChildren.Length; i++) {
            distancesToChildren[i] = (robotChildren[i].transform.position - transform.position).magnitude;
        }

        return robotChildren[ArrayUtility.IndexOf(distancesToChildren, distancesToChildren.Min())];
    }
}
