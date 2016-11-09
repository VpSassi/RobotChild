using UnityEngine;
using System.Collections;

public class Search : MonoBehaviour {

    Vector3[] searchPoints = new Vector3[5];

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SearchForPlayer() {
        float range = 15.0f;

        //if (searchPoints[4] == Vector3.zero) {
            for (int i = 0; i < 5; i++) {
                Vector3 randomPoint = Random.insideUnitSphere * range + transform.position;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas)) {
                    Debug.DrawLine(transform.position, hit.position, Color.red, 1f);
                    searchPoints[i] = hit.position;
                }
            }
        //}

        //move to first waypoint, loop through all waypoints
        //destPoint = (destPoint + 1) % points.Length;
        //navAgent.SetDestination(points[destPoint].position);
    }
}
