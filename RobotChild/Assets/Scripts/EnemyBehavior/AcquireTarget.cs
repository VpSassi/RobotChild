using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class AcquireTarget : MonoBehaviour {

    GameObject[] robotClones;
    GameObject[] robotChildren;
    float[] distancesToChildren;




    void Start() {
        robotClones = GameObject.FindGameObjectsWithTag("robotChild");
        robotChildren = new GameObject[robotClones.Length + 1];
        robotChildren[0] = GameObject.FindWithTag("Player");
        for (int i = 1; i <= robotClones.Length; i++) {
            robotChildren[i] = robotClones[i - 1];
        }
        distancesToChildren = new float[robotChildren.Length];
    }

    void Update() {
        //print(ClosestChild());
    }





    public GameObject ClosestChild() {
        for (int i = 0; i < distancesToChildren.Length; i++) {
            distancesToChildren[i] = (robotChildren[i].transform.position - transform.position).magnitude;
        }

        return robotChildren[ArrayUtility.IndexOf(distancesToChildren, distancesToChildren.Min())];
    }
}
