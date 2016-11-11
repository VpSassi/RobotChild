using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Search : MonoBehaviour {

    public float closeEnoughToWP = 1f;

    public List<Vector3> searchPoints = new List<Vector3>();
    bool[] hasWPBeenReached = new bool[5];






    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}






    public void SearchInRadius(NavMeshAgent agent, float range) {
        string s = " ";
        for (int i = 0; i < hasWPBeenReached.Length; i++) {
            s += " " + hasWPBeenReached[i].ToString();
                }
        print(s);

        if (searchPoints.Count == 0) {
            for (int i = 0; i < 5; i++) {
                Vector3 randomPoint = Random.insideUnitSphere * range + transform.position;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas)) {
                    Debug.DrawLine(transform.position, hit.position, Color.red, 20f);
                    searchPoints.Add(hit.position);
                }
            }
        }

        //An ugly way of making the agent go through the set of waypoints we just created. Would be a lot easier if I knew how to make a loop wait for a condition fullfillment.
        if (hasWPBeenReached[0] == false) {
            agent.SetDestination(searchPoints[0]);
        }
        if ((searchPoints[0] - agent.transform.position).magnitude < closeEnoughToWP) {
            hasWPBeenReached[0] = true;
        }
        if (hasWPBeenReached[0] == true && hasWPBeenReached[1] == false) {
            agent.SetDestination(searchPoints[1]);
        }
        if ((searchPoints[1] - agent.transform.position).magnitude < closeEnoughToWP) {
            hasWPBeenReached[1] = true;
        }
        if (hasWPBeenReached[0] == true && hasWPBeenReached[1] == true && hasWPBeenReached[2] == false) {
            agent.SetDestination(searchPoints[2]);
        }
        if ((searchPoints[2] - agent.transform.position).magnitude < closeEnoughToWP) {
            hasWPBeenReached[2] = true;
        }
        if (hasWPBeenReached[0] == true && hasWPBeenReached[1] == true && hasWPBeenReached[2] == true && hasWPBeenReached[3] == false) {
            agent.SetDestination(searchPoints[3]);
        }
        if ((searchPoints[3] - agent.transform.position).magnitude < closeEnoughToWP) {
            hasWPBeenReached[3] = true;
        }
        if (hasWPBeenReached[0] == true && hasWPBeenReached[1] == true && hasWPBeenReached[2] == true && hasWPBeenReached[3] == true && hasWPBeenReached[4] == false) {
            agent.SetDestination(searchPoints[4]);
        }
        if ((searchPoints[4] - agent.transform.position).magnitude < closeEnoughToWP && hasWPBeenReached[0] == true && hasWPBeenReached[1] == true && hasWPBeenReached[2] == true && hasWPBeenReached[3] == true) {
            hasWPBeenReached[4] = true;
            searchPoints.Clear();
            hasWPBeenReached = new bool[5];
        }
    }
}
