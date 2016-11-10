using UnityEngine;
using System.Collections;

public class MoveToLastPos : MonoBehaviour {

    bool lastPosReached;





	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}




    //Returns true once player's last position has been reached
    public bool MoveToPlayerLastPos(NavMeshAgent agent, Vector3 position) {
        if (lastPosReached == false) {
            agent.SetDestination(position);
        }

        if ((position - agent.transform.position).magnitude < 1f) {
            lastPosReached = true;
            return true;
        }
        else {
            return false;
        }
    }
}
